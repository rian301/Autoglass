using Dufry.Domain.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Autoglass.Domain.Core.Attributes;
using Autoglass.Domain.Interfaces;
using Autoglass.Domain.Models;
using Autoglass.Infra.CrossCutting.Identity;
using Autoglass.Infra.CrossCutting.Identity.Models;
using Autoglass.Infra.Data.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Autoglass.Infra.Data.Context
{
    public abstract class AuditableDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly IUser _user;
        private readonly string[] PropertiesToIgnore = { "ChangedAt", "UserIdChange" };

        public AuditableDbContext(DbContextOptions options, IUser user) : base(options)
        {
            _user = user;

            SetAuditLogProperty("ChangedAt", DateTime.Now);

            if (_user.Id.HasValue)
                SetAuditLogProperty("UserIdChange", _user.Id.Value);
        }

        public DbSet<LogAudit> LogAudit { get; set; }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var auditEntries = OnBeforeSaveChanges(_user.Id);
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await OnAfterSaveChanges(auditEntries);
            return result;
        }

        public override int SaveChanges()
        {
            var auditEntries = OnBeforeSaveChanges(_user.Id);
            var result = base.SaveChanges();
            OnAfterSaveChanges(auditEntries).GetAwaiter();
            return result;
        }

        private List<AuditEntry> OnBeforeSaveChanges(int? userId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is LogAudit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged || !IncludeEntity(entry))
                    continue;

                var auditEntry = new AuditEntry(entry, userId);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;

                    if (PropertiesToIgnore.Contains(propertyName))
                        continue;

                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.LogType = LogAuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.LogType = LogAuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified && property.OriginalValue?.ToString() != property.CurrentValue?.ToString())
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.LogType = LogAuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries.Where(w => !w.HasTemporaryProperties))
            {
                var log = auditEntry.ToAudit();
                if (string.IsNullOrEmpty(log.NewValues))
                    continue;
                LogAudit.Add(log);
            }

            return auditEntries.Where(w => w.HasTemporaryProperties).ToList();
        }

        private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                LogAudit.Add(auditEntry.ToAudit());
            }

            return SaveChangesAsync();
        }

        private void SetAuditLogProperty(string property, object value)
        {
            if (ChangeTracker.HasChanges())
            {
                var inserts = ChangeTracker.Entries().Where(x => x.State == EntityState.Added && x.Entity.GetType().GetProperty(property) != null);
                foreach (var item in inserts)
                {
                    item.Property(property).CurrentValue = value;
                    if (item.GetType().GetProperty(property) != null)
                        item.Property(property).CurrentValue = value;
                }

                var modifies = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified && x.Entity.GetType().GetProperty(property) != null);
                foreach (var item in modifies)
                {
                    item.Property(property).CurrentValue = value;
                }
            }
        }
        private bool IncludeEntity(object entry)
        {
            var includeAttr = entry.GetType().GetTypeInfo().GetCustomAttribute(typeof(AuditIgnoreAttribute), true);
            if (includeAttr != null)
                return false;

            return true;
        }
    }
}

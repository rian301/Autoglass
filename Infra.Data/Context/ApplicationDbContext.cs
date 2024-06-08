using Autoglass.Domain.Interfaces;
using Autoglass.Infra.CrossCutting.Identity.Models;
using Autoglass.Infra.CrossCutting.Identity;
using Autoglass.Infra.Data.Extensions;
using Autoglass.Infra.Data.Map;
using Domain.Models;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Autoglass.Infra.Data.Map;
using Autoglass.Domain.Models;

namespace Autoglass.Infra.Data.Context
{
    public class ApplicationDbContext : AuditableDbContext
    {
        #region Constructors
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUser user) : base(options, user)
        {
        }
        #endregion
        public DbSet<User> User { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<LoginRefreshToken> LoginRefreshToken { get; set; }
        public DbSet<UserProfilePermission> UserProfilePermission { get; set; }
        public DbSet<LogErro> LogErro { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<ValidationFailure>();
            builder.Ignore<ValidationResult>();

            builder.AddConfiguration(new ProdutoMap());

            builder.Entity<Role>(e => e.ToTable("Roles"));
            builder.Entity<RoleClaim>(e => e.ToTable("RoleClaim"));

            builder.AddConfiguration(new UserProfileMap());
            builder.AddConfiguration(new UserProfilePermissionMap());
            builder.AddConfiguration(new PermissionMap());

            builder.Entity<User>(e => e.ToTable("User"));
            builder.Entity<UserClaim>(e => e.ToTable("UserClaim"));
            builder.Entity<UserLogin>(e => e.ToTable("UserLogin"));
            builder.Entity<UserToken>(e => e.ToTable("UserToken"));
            builder.Entity<UserRole>(e => e.ToTable("UserRole"));

            base.OnModelCreating(builder);

            // Remove cascade mode
            var cascadeFKs = builder.Model.GetEntityTypes()
                                          .SelectMany(t => t.GetForeignKeys())
                                          .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}

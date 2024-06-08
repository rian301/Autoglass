
using Microsoft.EntityFrameworkCore;
using Autoglass.Domain.Core.Models;
using Autoglass.Domain.Core.Notifications;
using Autoglass.Infra.Data.Context;
using Autoglass.Repository.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoglass.Repository.Implement.Base
{
    public class RepositoryBaseCRUD<TEntity, TKey> : RepositoryBase<TEntity, TKey>, IRepositoryBaseCRUD<TEntity, TKey> where TEntity : Entity<TEntity, TKey>
    {
        #region Constructors
        public RepositoryBaseCRUD(ApplicationDbContext context, INotificationHandler<DomainNotification> notification) : base(context, notification)
        { }
        #endregion

        #region Methods
        public virtual void Update(TEntity obj)
        {
            if (!obj.IsValid())
            {
                _notification.Handle(obj.ValidationResult.Errors);
                return;
            }

            dbSet.Update(obj);
        }

        public virtual void Add(TEntity obj)
        {
            if (!obj.IsValid())
            {
                _notification.Handle(obj.ValidationResult.Errors);
                return;
            }

            dbSet.Add(obj);
        }

        public virtual Task<TEntity> GetByIdAsync(TKey id)
        {
            return dbSet.FindAsync(id).AsTask();
        }

        public virtual TEntity GetById(TKey id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet.AsNoTracking().ToList();
        }

        public virtual Task<List<TEntity>> GetAllAsync()
        {
            return dbSet.AsNoTracking().ToListAsync();
        }

        public virtual IQueryable<TEntity> Table()
        {
            return dbSet;
        }

        public virtual IQueryable<TEntity> TableNoTracking()
        {
            return dbSet.AsNoTracking();
        }

        public virtual void Delete(TKey id)
        {
            dbSet.Remove(dbSet.Find(id));
        }

        public virtual void DeleteAll()
        {
            dbSet.RemoveRange(dbSet);
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        #endregion
    }
}

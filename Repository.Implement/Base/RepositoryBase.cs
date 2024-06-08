using Autoglass.Domain.Core.Models;
using Autoglass.Domain.Core.Notifications;
using Autoglass.Infra.CrossCutting.Identity.Models;
using Autoglass.Infra.CrossCutting.Identity;
using Autoglass.Infra.Data.Context;
using Autoglass.Repository.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Autoglass.Repository.Implement.Base
{
    public class RepositoryBase : IRepositoryBase
    {

        #region Properties

        private readonly IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken> DbContext;
        protected ApplicationDbContext Context { get { return (ApplicationDbContext)this.DbContext; } }
        //protected readonly DbSet<T> dbSet;
        protected readonly INotificationHandler<DomainNotification> _notification;

        #endregion

        #region Constructors

        public RepositoryBase(ApplicationDbContext entityContext, INotificationHandler<DomainNotification> notification)
        {
            DbContext = entityContext;
            //dbSet = Context.Set<TEntity>();
            _notification = notification;
        }

        public RepositoryBase()
        {
        }

        #endregion

    }

    public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : Entity<TEntity, TKey>
    {
        #region Properties
        private readonly IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken> DbContext;
        protected readonly DbSet<TEntity> dbSet;
        protected ApplicationDbContext Context { get { return (ApplicationDbContext)this.DbContext; } }
        protected readonly INotificationHandler<DomainNotification> _notification;
        #endregion

        #region Constructors
        public RepositoryBase(ApplicationDbContext contextoEntity, INotificationHandler<DomainNotification> notification)
        {
            DbContext = contextoEntity;
            dbSet = Context.Set<TEntity>();
            _notification = notification;
        }
        #endregion
    }

    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : Entity<TEntity>
    {
        #region Properties
        private readonly IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken> DbContext;
        protected readonly DbSet<TEntity> dbSet;
        protected ApplicationDbContext Context { get { return (ApplicationDbContext)this.DbContext; } }
        protected readonly INotificationHandler<DomainNotification> _notification;
        #endregion

        #region Constructors
        public RepositoryBase(ApplicationDbContext contextoEntity, INotificationHandler<DomainNotification> notification)
        {
            DbContext = contextoEntity;
            dbSet = Context.Set<TEntity>();
            _notification = notification;
        }
        #endregion

    }
}

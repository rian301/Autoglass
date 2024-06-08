using Autoglass.Domain.Core.Models;
using Autoglass.Domain.Core.Notifications;
using Autoglass.Repository.Base;
using Autoglass.Service.Base;

namespace Autoglass.Service.Implement.Base
{
    public class ServiceBase
    {
        #region Properties
        protected readonly INotificationHandler<DomainNotification> _notification;
        #endregion

        #region Constructors
        public ServiceBase(IRepositoryBase repository, INotificationHandler<DomainNotification> notification)
        {
            _notification = notification;
        }
        #endregion
    }
    public class ServiceBase<TEntity, TKey> : IServiceBase<TEntity, TKey> where TEntity : Entity<TEntity, TKey>
    {
        #region Properties
        private readonly IRepositoryBase<TEntity, TKey> _repository;
        protected readonly INotificationHandler<DomainNotification> _notification;
        #endregion

        #region Constructors
        public ServiceBase(IRepositoryBase<TEntity, TKey> repository, INotificationHandler<DomainNotification> notification)
        {
            _repository = repository;
            _notification = notification;
        }
        #endregion
    }

    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : Entity<TEntity>
    {
        #region Properties
        private readonly IRepositoryBase<TEntity> _repository;
        protected readonly INotificationHandler<DomainNotification> _notification;
        #endregion

        #region Constructors
        public ServiceBase(IRepositoryBase<TEntity> repository, INotificationHandler<DomainNotification> notification)
        {
            _repository = repository;
            _notification = notification;
        }
        #endregion
    }
}

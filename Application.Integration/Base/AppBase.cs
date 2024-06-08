using Autoglass.Application.Base;
using Autoglass.Domain.Core.Models;
using Autoglass.Domain.Core.Notifications;
using Autoglass.Domain.Interfaces;
using Autoglass.Service.Base;

namespace Autoglass.Application.Implement.Base
{
    public class AppBase : IAppBase
    {
        #region Properties
        private readonly IServiceBase _service;
        protected readonly INotificationHandler<DomainNotification> _notification;
        protected readonly IUser _user;
        #endregion

        #region Constructors
        public AppBase(IServiceBase service, INotificationHandler<DomainNotification> notification, IUser user)
        {
            _service = service;
            _notification = notification;
            _user = user;
        }
        #endregion
    }
    public class AppBase<TEntity, TKey> : IAppBase<TEntity, TKey> where TEntity : Entity<TEntity, TKey>
    {

        #region Properties
        private readonly IServiceBase<TEntity, TKey> _service;
        protected readonly INotificationHandler<DomainNotification> _notification;
        protected readonly IUser _user;
        #endregion

        #region Constructors
        public AppBase(IServiceBase<TEntity, TKey> service, INotificationHandler<DomainNotification> notification, IUser user)
        {
            _service = service;
            _notification = notification;
            _user = user;
        }
        #endregion
    }

    public class AppBase<TEntity> : IAppBase<TEntity> where TEntity : Entity<TEntity>
    {
        #region Properties
        private readonly IServiceBase<TEntity> _service;
        protected readonly INotificationHandler<DomainNotification> _notification;
        protected readonly IUser _user;
        #endregion

        #region Constructors
        public AppBase(IServiceBase<TEntity> service, INotificationHandler<DomainNotification> notification, IUser user)
        {
            _service = service;
            _notification = notification;
            _user = user;
        }
        #endregion

    }
}

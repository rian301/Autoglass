using Autoglass.Domain.Core.Models;
using Autoglass.Domain.Core.Notifications;
using Autoglass.Repository.Base;
using Autoglass.Service.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autoglass.Service.Implement.Base
{
    public class ServiceBaseCRUD<TEntity, TKey> : IServiceBaseCRUD<TEntity, TKey> where TEntity : Entity<TEntity, TKey>
    {
        #region Properties
        private readonly IRepositoryBaseCRUD<TEntity, TKey> _repository;
        protected readonly INotificationHandler<DomainNotification> _notification;
        #endregion

        #region Constructors
        public ServiceBaseCRUD(IRepositoryBaseCRUD<TEntity, TKey> repository, INotificationHandler<DomainNotification> notification)
        {
            _repository = repository;
            _notification = notification;
        }
        #endregion

        #region Methods

        public virtual void Add(TEntity obj) => _repository.Add(obj);
        public virtual void Update(TEntity obj) => _repository.Update(obj);
        public virtual Task<TEntity> GetByIdAsync(TKey id) => _repository.GetByIdAsync(id);
        public virtual TEntity GetById(TKey id) => _repository.GetById(id);
        public virtual IEnumerable<TEntity> GetAll() => _repository.GetAll();
        public virtual Task<List<TEntity>> GetAllAsync() => _repository.GetAllAsync();
        public virtual void Delete(TKey id) => _repository.Delete(id);
        public void DeleteAllAsync() => _repository.DeleteAll();

        #endregion
    }
}

using Autoglass.Domain.Core.Notifications;
using Autoglass.Domain.Interfaces;
using Autoglass.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Autoglass.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Properties
        private readonly DbContext _dbContext;
        private readonly INotificationHandler<DomainNotification> _notification;
        private IDbContextTransaction _transaction;
        #endregion

        #region Constructors
        public UnitOfWork(ApplicationDbContext context, INotificationHandler<DomainNotification> notification)
        {
            _dbContext = context;
            _notification = notification;
        }
        #endregion

        #region Methods
        public bool Save()
        {
            if (_notification.HasNotifications()) return false;

            return _dbContext.SaveChanges() > 0;
        }

        public async Task<bool> SaveAsync()
        {
            if (_notification.HasNotifications()) return false;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
        #endregion
    }
}

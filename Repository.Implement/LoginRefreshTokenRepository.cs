using Autoglass.Domain.Core.Notifications;
using Autoglass.Domain.Models;
using Autoglass.Infra.Data.Context;
using Autoglass.Repository.Implement.Base;
using Procard.Repository;
using System;
using System.Linq;

namespace Autoglass.Repository.Implement
{
    public class LoginRefreshTokenRepository : RepositoryBaseCRUD<LoginRefreshToken, string>, ILoginRefreshTokenRepository
    {
        #region Construtor
        public LoginRefreshTokenRepository(ApplicationDbContext context, INotificationHandler<DomainNotification> notification) : base(context, notification)
        {
        }
        #endregion

        public LoginRefreshToken GetByUserToken(int idUser, string token)
        {
            var obj = Context.LoginRefreshToken.Where(w => w.IdUser == idUser && w.Id == token && w.DueDate > DateTime.Now)
                           .FirstOrDefault();
            return obj;
        }

    }
}

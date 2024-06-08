using Autoglass.Domain.Core.Notifications;
using Autoglass.Domain.Models;
using Autoglass.Repository;
using Autoglass.Service.Implement.Base;
using Procard.Repository;

namespace Autoglass.Service.Implement
{
    public class LoginRefreshTokenService : ServiceBaseCRUD<LoginRefreshToken, string>, ILoginRefreshTokenService
    {
        #region Properties
        private readonly ILoginRefreshTokenRepository _repository;
        #endregion

        #region Constructors
        public LoginRefreshTokenService(ILoginRefreshTokenRepository repository, INotificationHandler<DomainNotification> notification) : base(repository, notification)
        {
            _repository = repository;
        }
        #endregion

        public LoginRefreshToken GetByUserToken(int codigoUsuario, string token)
        {
            return _repository.GetByUserToken(codigoUsuario, token);
        }
    }
}

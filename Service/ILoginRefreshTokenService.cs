using Autoglass.Domain.Models;
using Autoglass.Service.Base;

namespace Autoglass.Service
{
    public interface ILoginRefreshTokenService : IServiceBaseCRUD<LoginRefreshToken, string>
    {
        LoginRefreshToken GetByUserToken(int idUser, string token);
    }
}

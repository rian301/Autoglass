using Autoglass.Domain.Models;
using Autoglass.Repository.Base;

namespace Procard.Repository
{
    public interface ILoginRefreshTokenRepository : IRepositoryBaseCRUD<LoginRefreshToken, string>
    {
        LoginRefreshToken GetByUserToken(int idUser, string token);
    }
}

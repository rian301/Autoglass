using System.Collections.Generic;
using System.Security.Claims;

namespace Autoglass.Domain.Interfaces
{
    public interface IUser
    {
        IEnumerable<Claim> Claims { get; }
        int? Id { get; }
        string Name { get; }
        string Email { get; }
        bool IsAuthenticated { get; }
        int? CustomerId { get; }
    }
}

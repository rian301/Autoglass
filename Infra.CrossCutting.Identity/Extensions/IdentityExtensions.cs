using Autoglass.Infra.CrossCutting.Identity;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Identity
{
    public static class IdentityExtensions
    {
        public static async Task<User> FindByNameOrEmailAsync
            (this UserManager<User> userManager, string usernameOrEmail)
        {
            var username = usernameOrEmail;
            if (usernameOrEmail.Contains("@"))
            {
                var userForEmail = await userManager.FindByEmailAsync(usernameOrEmail);
                if (userForEmail != null)
                    username = userForEmail.UserName;
            }
            return await userManager.FindByNameAsync(username);
        }
    }
}

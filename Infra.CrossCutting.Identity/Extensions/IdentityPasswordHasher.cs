using Autoglass.Infra.CrossCutting.Common.Seguranca;
using Microsoft.AspNetCore.Identity;

namespace Autoglass.Infra.CrossCutting.Identity.Extensions
{
    public class IdentityPasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class
    {
        #region Methods
        public string HashPassword(TUser user, string password)
        {
            return Encrypt.ObterMd5Hash(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            providedPassword = Encrypt.ObterMd5Hash(providedPassword);
            return hashedPassword.Equals(providedPassword) ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
        #endregion
    }
}

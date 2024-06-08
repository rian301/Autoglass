using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Autoglass.Infra.Framework.Web.Authentication
{
    public class SigningConfigurations
    {
        #region Properties
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }
        #endregion

        #region Constructors
        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.RsaSha256Signature);
        }
        #endregion
    }
}

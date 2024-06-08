namespace Autoglass.Infra.Framework.Web.Authentication
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
        public string Key { get; set; }
    }
}

namespace Autoglass.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class ExternalLoginSchemeViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }
}

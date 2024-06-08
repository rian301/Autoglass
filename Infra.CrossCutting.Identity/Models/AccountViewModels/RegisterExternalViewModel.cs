namespace Autoglass.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class RegisterExternalViewModel
    {
        public string provider { get; set; }
        public string id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string photoUrl { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
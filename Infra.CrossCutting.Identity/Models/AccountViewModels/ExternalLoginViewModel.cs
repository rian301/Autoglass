using System.ComponentModel.DataAnnotations;

namespace Autoglass.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Provider { get; set; }
        public string ProviderKey { get; set; }
    }
}

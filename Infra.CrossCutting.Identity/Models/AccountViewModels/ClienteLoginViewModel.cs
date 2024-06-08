using System.ComponentModel.DataAnnotations;

namespace Autoglass.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class ClienteLoginViewModel
    {
        [Required]        
        public string EmailOrUsername { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }        
    }
}

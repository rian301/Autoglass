using System.ComponentModel.DataAnnotations;

namespace Autoglass.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]        
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

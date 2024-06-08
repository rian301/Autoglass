using System.ComponentModel.DataAnnotations;

namespace Autoglass.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class LoginRefreshTokenViewModel
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public int IdUser { get; set; }

        public int? IdAccount { get; set; }
    }
}

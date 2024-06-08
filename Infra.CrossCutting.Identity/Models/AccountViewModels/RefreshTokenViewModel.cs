using System.ComponentModel.DataAnnotations;

namespace Autoglass.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class RefreshTokenViewModel
    {
        [Required]
        public string RefreshToken { get; set; }
        [Required]
        public int CodigoLogin { get; set; }
    }
}

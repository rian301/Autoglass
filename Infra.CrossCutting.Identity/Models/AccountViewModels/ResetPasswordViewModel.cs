using System.ComponentModel.DataAnnotations;

namespace Autoglass.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "{0} inválido")]
        public string Email { get; set; }


        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "A {0} precisa ser maior ou igual à 6 caracteres.", MinimumLength = 6)]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Password { get; set; }

        [Display(Name = "Confirmar Senha")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Senha e confirmação de senha estão diferentes.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}

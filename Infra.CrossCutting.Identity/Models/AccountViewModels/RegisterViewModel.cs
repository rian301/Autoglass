using System.ComponentModel.DataAnnotations;

namespace Autoglass.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
        }

        public RegisterViewModel(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public RegisterViewModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Name { get; set; }

        //[Display(Name = "CPF")]
        //[CustomValidationCPF(ErrorMessage = "{0} inválido")]
        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        //public string CPF { get; set; }

        //[Display(Name = "Telefone")]
        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        //public string PhoneNumber { get; set; }

        public int? UserProfileId { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "{0} inválido")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Password { get; set; }

        [Display(Name = "Confirmar Senha")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Senha e confirmação de senha estão diferentes.")]
        public string ConfirmPassword { get; set; }

        public RegisterExternalViewModel LoginExterno { get; set; }
    }
}

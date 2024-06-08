using System.ComponentModel.DataAnnotations;

namespace Autoglass.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class UserUpdateViewModel
    {
        #region Properties
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Name { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "{0} inválido")]
        public string Email { get; set; }

        //[Display(Name = "CPF")]
        //[CustomValidationCPF(ErrorMessage = "{0} inválido")]
        //[Required(ErrorMessage = "O campo {0} é obrigatório")]        
        //public string CPF { get; set; }

        //[Display(Name = "Telefone")]
        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        //[CustomValidationTelefoneFixoCelular(ErrorMessage = "{0} inválido")]
        //public string PhoneNumber { get; set; }

        [Display(Name = "Ativo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool Active { get; set; }

        [Display(Name = "Perfil")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int? UserProfileId { get; set; }            
        #endregion
    }
}

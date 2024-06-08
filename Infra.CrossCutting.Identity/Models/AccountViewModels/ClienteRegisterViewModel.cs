using System;
using System.ComponentModel.DataAnnotations;

namespace Autoglass.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class ClienteRegisterViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Nome do usuário é obrigatório")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "Data de Nascimento é obrigatório")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Sexo é obrigatório")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "Celular é obrigatório")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Confirme a senha é obrigatório")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "Senha e confirmação de senha estão diferentes.")]
        public string ConfirmeSenha { get; set; }     
        
        public RegisterExternalViewModel LoginExterno { get; set; }
    }
}

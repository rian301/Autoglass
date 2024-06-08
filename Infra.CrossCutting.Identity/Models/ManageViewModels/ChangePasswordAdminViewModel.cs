﻿using System.ComponentModel.DataAnnotations;

namespace Autoglass.Infra.CrossCutting.Identity.Models.ManageViewModels
{
    public class ChangePasswordAdminViewModel
    {
        [Required]
        [Display(Name = "Id Usuário")]
        public string UserId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A senha precisa ter no mínimo 6 caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("NewPassword", ErrorMessage = "A nova senha e a confirmação não são iguais.")]
        public string ConfirmPassword { get; set; }        
    }
}

using FluentValidation;
using Autoglass.Domain.Core.Attributes;
using Autoglass.Domain.Core.Models;
using System;

namespace Autoglass.Domain.Models
{
    [AuditIgnore]
    public class LoginRefreshToken : Entity<LoginRefreshToken, string>
    {

        #region Properties
        public int IdUser { get; private set; }
        public string Token { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime DueDate { get; private set; }
        #endregion

        #region Constructors

        protected LoginRefreshToken()
        {
        }

        public LoginRefreshToken(string refreshTokenCode, int idUser, string token, DateTime dataValidade)
        {
            Id = refreshTokenCode;
            IdUser = idUser;
            Token = token;
            DueDate = dataValidade;
            CreatedAt = DateTime.Now;
        }

        #endregion

        #region Validators
        public override bool IsValid()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Token é obrigatório");
            RuleFor(c => c.Token).NotEmpty().WithMessage("Token é obrigatório");
            RuleFor(c => c.IdUser).NotEmpty().WithMessage("Vincular o usuário é obrigatório");
            RuleFor(c => c.DueDate).NotNull().WithMessage("Data de validade é obrigatório");
            RuleFor(c => c.CreatedAt).NotNull().WithMessage("Data de inclusão é obrigatório");

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
        #endregion

    }
}

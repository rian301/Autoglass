using Autoglass.Domain.Core.Models;
using FluentValidation;
using System;
using Autoglass.Domain.Core.Attributes;

namespace Autoglass.Domain.Models
{
    [AuditIgnore]
    public class LogErro : Entity<LogErro, int>
    {
        #region Properties
        public DateTime LogDate { get; private set; }
        public int System { get; private set; }
        public string Class { get; private set; }
        public string Context { get; private set; }
        public string Request { get; private set; }
        public string Error { get; private set; }

        #endregion

        #region Constructors

        protected LogErro() { }

        public LogErro(int system, string classe, string error, string context = null, string request = null)
        {
            LogDate = DateTime.Now;
            System = system;
            Class = classe;
            Context = context;
            Request = request;
            Error = error;
        }
        #endregion

        #region Methods
        public override bool IsValid()
        {
            RuleFor(c => c.Class)
                .NotEmpty().WithMessage("Classe que ocorreu o erro é obrigatório")
                .MaximumLength(300).WithMessage("Classe que ocorreu o erro não pode ter mais de 300 caracteres.");

            RuleFor(c => c.Error)
                .NotEmpty().WithMessage("Mensagem de Erro é obrigatório");

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
        #endregion
    }
}

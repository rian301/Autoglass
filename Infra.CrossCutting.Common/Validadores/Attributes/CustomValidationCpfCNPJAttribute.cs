using Autoglass.Infra.CrossCutting.Common.String;
using System.ComponentModel.DataAnnotations;

namespace Autoglass.Infra.CrossCutting.Common.Validadores.Attributes
{
    public class CustomValidationCpfCNPJAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            return Validador.ValidarCpfCNPJ(value.ToString().RemoverCaracterMascara());
        }
    }
}

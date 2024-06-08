using System.ComponentModel.DataAnnotations;

namespace Autoglass.Infra.CrossCutting.Common.Validadores.Attributes
{
    public class CustomValidationCPFAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            return Validador.ValidarCPF(value.ToString());
        }
    }
}

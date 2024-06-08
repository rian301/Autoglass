using System.ComponentModel.DataAnnotations;

namespace Autoglass.Infra.CrossCutting.Common.Validadores.Attributes
{
    public class CustomValidationTelefoneFixoCelularAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            return (Validador.ValidarTelefoneFixo(value.ToString()) || Validador.ValidarTelefoneCelular(value.ToString()));
        }
    }
}

using System;
using System.Text.RegularExpressions;

namespace Autoglass.Infra.CrossCutting.Common.Validadores
{
    public static class Validador
    {
        #region Methods
        public static bool ValidarTelefoneFixo(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            string phonePattern = @"^"
                        + @"(?<areaCode>[(]?\d{1,3}[)]?\s?)?"
                        + @"(?<numero>\d{3,5}[-]?\d{4})"
                        + @"$";

            Regex phoneValidator = new Regex(phonePattern);

            return (phoneValidator.IsMatch(input));
        }

        public static bool ValidarTelefoneCelular(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            string phonePattern = @"^"
                        + @"(?<areaCode>[(]?\d{1,4}[)]?\s?)?"
                        + @"(?<numero>\d{3,5}[-]?\d{4})"
                        + @"$";

            Regex phoneValidator = new Regex(phonePattern);

            return (phoneValidator.IsMatch(input));
        }

        public static bool ValidarCpfCNPJ(this string input)
        {
            return (ValidarCPF(input) || ValidarCNPJ(input));
        }

        public static bool ValidarCPF(this string input)
        {
            string numeros, digitos;
            bool digitos_iguais = true;
            if (input.Length < 11)
                return false;

            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input[i].ToString() != input[i + 1].ToString())
                {
                    digitos_iguais = false;
                    break;
                }
            }

            if (!digitos_iguais)
            {
                numeros = input.Substring(0, 9);
                digitos = input.Substring(9);
                int soma = 0;
                for (int i = 10; i > 1; i--)
                    soma += Convert.ToInt32(numeros[10 - i].ToString()) * i;
                var resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                if (resultado != Convert.ToInt32(digitos[0].ToString()))
                    return false;
                numeros = input.Substring(0, 10);
                soma = 0;
                for (int i = 11; i > 1; i--)
                    soma += Convert.ToInt32(numeros[11 - i].ToString()) * i;
                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                if (resultado != Convert.ToInt32(digitos[1].ToString()))
                    return false;
                return true;
            }
            else
                return false;
        }

        public static bool ValidarCNPJ(this string input)
        {
            string cnpj = Regex.Replace(input, @"/[^\d] +/ g", "");

            if (cnpj == "") return false;

            if (cnpj.Length != 14)
                return false;

            // Elimina CNPJs invalidos conhecidos
            if (cnpj == "00000000000000" ||
                cnpj == "11111111111111" ||
                cnpj == "22222222222222" ||
                cnpj == "33333333333333" ||
                cnpj == "44444444444444" ||
                cnpj == "55555555555555" ||
                cnpj == "66666666666666" ||
                cnpj == "77777777777777" ||
                cnpj == "88888888888888" ||
                cnpj == "99999999999999")
                return false;

            // Valida DVs
            var tamanho = cnpj.Length - 2;
            var numeros = cnpj.Substring(0, tamanho);
            var digitos = cnpj.Substring(tamanho);
            var soma = 0;
            var pos = tamanho - 7;
            for (var i = tamanho; i >= 1; i--)
            {
                soma += Convert.ToInt32(numeros[tamanho - i].ToString()) * pos--;
                if (pos < 2)
                    pos = 9;
            }
            var resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
            if (resultado != Convert.ToInt32(digitos[0].ToString()))
                return false;

            tamanho = tamanho + 1;
            numeros = cnpj.Substring(0, tamanho);
            soma = 0;
            pos = tamanho - 7;
            for (int i = tamanho; i >= 1; i--)
            {
                soma += Convert.ToInt32(numeros[tamanho - i].ToString()) * pos--;
                if (pos < 2)
                    pos = 9;
            }
            resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
            if (resultado != Convert.ToInt32(digitos[1].ToString()))
                return false;

            return true;
        }

        public static bool ValidarEmail(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            string phonePattern = @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$";
            Regex validatorRegex = new Regex(phonePattern);

            return (validatorRegex.IsMatch(input));
        }

        public static bool ValidarDataNascimento(this DateTime? input)
        {

            if (!input.HasValue)
                return false;

            var result = true;

            DateTime date;

            //"Data de nascimento inválida.";
            if (!DateTime.TryParse(input.ToString(), out date) || Convert.ToInt32(input.Value.Year.ToString()).ToString().Length <= 3)
                result = false;

            //Não é permitido data de nascimento futura.
            if (input.Value > DateTime.Now)
                result = false;

            return result;
        }

        public static bool ValidarPlacaDetran(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            var regex = @"^[a-zA-Z]{3}\d{1}[a-zA-Z0-9]{1}\d{2}$"; // Placa detran de mercosul
            var match = Regex.Match(input, regex, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;

            return false;
        }

        public static bool ValidarPlacaContainer(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            var regex = @"^\w{3}U\d{7}$";
            var match = Regex.Match(input, regex, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;

            return false;
        }
    } 
    #endregion
}
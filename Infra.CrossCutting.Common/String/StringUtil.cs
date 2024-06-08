using Autoglass.Infra.CrossCutting.Common.Validadores;
using System.Text.RegularExpressions;

namespace Autoglass.Infra.CrossCutting.Common.String
{
    public static class StringUtil
    {
        #region Methods
        public static string RemoverCaracterMascara(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input.Replace("-", "").Replace("/", "").Replace("(", "").Replace(")", "").Replace(".", "").Replace(" ", "").Replace("%", "").Replace(",", "").Replace(".", "");
        }

        public static string RemoverAcentuacao(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string pattern = @"(?i)[^0-9a-záéíóúàèìòùâêîôûãõç\s]";

            Regex regex = new Regex(pattern);

            return regex.Replace(input, "");
        }

        public static string GetContainerCode(string text)
        {
            text = text.Replace("\n", "").Replace(" ", "").RemoverCaracterMascara().RemoverAcentuacao().ToUpper();
            var result = Validador.ValidarPlacaContainer(text);
            if (result)
                return text;
            else 
                return null;
        }

        public static string GetDetranCode(string text)
        {
            text = text.Replace("\n", "").Replace(" ", "").RemoverCaracterMascara().RemoverAcentuacao().ToUpper();
            var result = Validador.ValidarPlacaDetran(text);
            if (result)
                return text;
            else
                return null;
        }

        public static string FirstCharacters(string text, int quantity)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            if (quantity > text.Length)
                return null;

            return text.Substring(0, quantity);
        }

        public static string LastCharacters(string text, int quantity)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            if (text.Length < quantity)
                return null;

            return text.Substring(text.Length - quantity, quantity);
        }

        public static string GetFirstName(this string fullname)
        {
            if (string.IsNullOrEmpty(fullname))
                return null;

            return fullname.Trim().Split(" ")[0];
        }
        #endregion
    }
}

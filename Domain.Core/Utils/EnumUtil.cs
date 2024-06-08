using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autoglass.Domain.Core.Utils
{
    public static class EnumUtil
    {
        /// <summary>
        /// The get enum description.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="TEnum"> Tipo de enum a ser atribuido.
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetValue<TEnum>(this TEnum value, Type type)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            var attributes = (AttributeEnum[])fi.GetCustomAttributes(typeof(AttributeEnum), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Valor;
            }

            return value.ToString();
        }

        /// <summary>
        /// The get enum description.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="TEnum"> Tipo de enum a ser atribuido.
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetDescriptions<TEnum>(this TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            if (fi == null) return null;

            var attributes = (AttributeEnum[])fi.GetCustomAttributes(typeof(AttributeEnum), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            return value.ToString();
        }

        /// <summary>
        /// The get enum description.
        /// </summary>
        /// <param name="valor">
        /// The value.
        /// </param>
        /// <typeparam name="TEnum"> Tipo de enum a ser atribuido.
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static TEnum GetEnumByValue<TEnum>(string valor)
        {
            Type enumType = typeof(TEnum);

            TEnum enumObjectType = default(TEnum);

            foreach (Enum val in enumType.GetEnumValues())
            {
                FieldInfo fi = enumType.GetField(val.ToString());

                AttributeEnum[] attributes = (AttributeEnum[])fi.GetCustomAttributes(typeof(AttributeEnum), false);

                AttributeEnum attr = attributes[0];

                if (attr.Valor.ToUpper() == valor.ToUpper())
                {
                    enumObjectType = (TEnum)Enum.Parse(typeof(TEnum), val.ToString());

                    break;
                }
            }

            return enumObjectType;
        }

        /// <summary>
        /// The get enum description.
        /// </summary>
        /// <param name="descricao">
        /// The value.
        /// </param>
        /// <typeparam name="TEnum"> Tipo de enum a ser atribuido.
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static TEnum GetEnumByDescription<TEnum>(string description)
        {
            Type enumType = typeof(TEnum);

            TEnum enumObjectType = default(TEnum);

            foreach (Enum val in enumType.GetEnumValues())
            {
                FieldInfo fi = enumType.GetField(val.ToString());

                AttributeEnum[] attributes = (AttributeEnum[])fi.GetCustomAttributes(typeof(AttributeEnum), false);

                AttributeEnum attr = attributes[0];

                if (attr.Description.ToUpper() == description.ToUpper())
                {
                    enumObjectType = (TEnum)Enum.Parse(typeof(TEnum), val.ToString());

                    break;
                }
            }

            return enumObjectType;
        }

        /// <summary>
        /// The obter data source.
        /// </summary>
        /// <param name="enumType">
        /// The enum Type.
        /// </param>
        /// <returns>
        /// Retorna o enum formatada em uma lista tipada. <see cref="List"/>.
        /// </returns>
        public static List<AttributeEnum> ListarEnums(this Type enumType)
        {
            var enuns = new List<AttributeEnum>();

            foreach (Enum val in enumType.GetEnumValues())
            {
                FieldInfo fi = enumType.GetField(val.ToString());

                AttributeEnum[] attributes = (AttributeEnum[])fi.GetCustomAttributes(typeof(AttributeEnum), false);

                if (attributes.Any())
                {
                    AttributeEnum attr = attributes[0];

                    if (attr.Show)
                        enuns.Add(new AttributeEnum(attr.Valor, attr.Description, attr.Show, attr.Type));
                }
            }

            return enuns;
        }
    }
}

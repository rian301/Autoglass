using System;

namespace Autoglass.Domain.Core.Utils
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AttributeEnum : Attribute
    {
        /// <summary>
        /// Inicia uma nova instância da classe <see cref="AttributeEnum"/>.
        /// </summary>
        /// <param name="valor">
        /// The valor.
        /// </param>
        /// <param name="description">
        /// The descricao.
        /// </param>
        /// <param name="show">
        /// The exibir.
        /// </param>
        public AttributeEnum(string valor, string description, bool show, string type = null)
        {
            Valor = valor;
            Description = description;
            Show = show;
            Type = type;
        }

        /// <summary>
        /// Obtém ou define the codigo.
        /// </summary>
        /// <value>
        /// The codigo.
        /// </value>
        public string Valor { get; set; }

        /// <summary>
        /// Obtém ou define the descricao.
        /// </summary>
        /// <value>
        /// The descricao.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Obtém ou define um valor que indica se pode ou não exibir no controle.
        /// </summary>
        /// <value>
        /// The exibir.
        /// </value>
        public bool Show { get; set; }

        /// <summary>
        /// Obtém ou define um valor que indica se pode ou não exibir no controle.
        /// </summary>
        /// <value>
        /// The Tipo.
        /// </value>
        public string Type { get; set; }
    }
}

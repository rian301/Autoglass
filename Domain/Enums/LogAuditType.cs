using Autoglass.Domain.Core.Utils;

namespace Dufry.Domain.Enums
{
    public enum SituacaoProduto
    {
        [AttributeEnum("0", "Ativo", true)]
        Ativo = 0,
        [AttributeEnum("1", "Inativo", true)]
        Inativo = 1,
    }
}

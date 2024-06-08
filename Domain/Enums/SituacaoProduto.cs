using Autoglass.Domain.Core.Utils;

namespace Dufry.Domain.Enums
{
    public enum LogAuditType
    {
        [AttributeEnum("0", "Nenhum", true)]
        None = 0,
        [AttributeEnum("1", "Inserido", true)]
        Create = 1,
        [AttributeEnum("2", "Alterado", true)]
        Update = 2,
        [AttributeEnum("3", "Deletado", true)]
        Delete = 3
    }
}

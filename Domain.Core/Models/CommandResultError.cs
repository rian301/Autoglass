namespace Autoglass.Domain.Core.Models
{
    public class CommandResultError
    {
        #region Properties
        public string Property { get; set; }
        public string Message { get; set; }
        #endregion

        #region Constructors
        public CommandResultError(string property, string message)
        {
            Property = property;
            Message = message;
        }
        #endregion
    }
}

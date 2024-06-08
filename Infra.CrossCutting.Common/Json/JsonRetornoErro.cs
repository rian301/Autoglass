namespace Autoglass.Infra.CrossCutting.Common.Json
{
    public class JsonRetornoErro
    {
        #region Properties
        public string Property { get; set; }
        public string Message { get; set; }
        #endregion

        #region Constructors
        public JsonRetornoErro(string property, string message)
        {
            this.Property = property;
            this.Message = message;
        }

        public JsonRetornoErro(string message)
        {
            this.Message = message;
        } 
        #endregion
    }
}

using Autoglass.Domain.Models;
using Autoglass.Service;

namespace Autoglass.Application.Implement
{
    public class LogErroApp : ILogErroApp
    {
        #region Properties
        private readonly ILogErroService _service;
        #endregion

        #region Constructors
        public LogErroApp(ILogErroService service)
        {
            _service = service;
        }
        #endregion

        #region Methods
        public void Insert(LogErro obj)
        {
            _service.Insert(obj);
        }
        #endregion
    }
}

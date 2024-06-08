using Autoglass.Service;

namespace Autoglass.Application.Implement
{
    public class PermissionValidateApp : IPermissionValidateApp
    {
        #region Properties
        private readonly IPermissionValidateService _service;
        #endregion

        #region Constructors

        public PermissionValidateApp(IPermissionValidateService service)
        {
            _service = service;
        }
        #endregion

        #region Methods
        public bool ValidateUserPermission(int idUser, string[] constPermission)
        {
            return _service.ValidateUserPermission(idUser, constPermission);
        }

        public bool ValidateUserPermission(int idUser, string constPermission)
        {
            return _service.ValidateUserPermission(idUser, constPermission);
        }
        #endregion
    }
}

using Autoglass.Repository;

namespace Autoglass.Service.Implement
{
    public class PermissionValidateService : IPermissionValidateService
    {
        #region Properties
        private readonly IPermissionValidateRepository _repository;
        #endregion

        #region Constructors
        public PermissionValidateService(IPermissionValidateRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods

        public bool ValidateUserPermission(int idUser, string[] constPermission)
        {
            return _repository.ValidateUserPermission(idUser, constPermission);
        }

        public bool ValidateUserPermission(int idUser, string constPermission)
        {
            return _repository.ValidateUserPermission(idUser, constPermission);
        }
        #endregion
    }
}

using Autoglass.Domain.Core.Models;

namespace Autoglass.Domain.Models
{
    public class UserProfilePermission : EntityLog<UserProfilePermission>
    {
        #region Properties
        public int UserProfileId { get; private set; }
        public int PermissionId { get; private set; }

        public virtual UserProfile UserProfile { get; set; }
        public virtual Permission Permission { get; set; }
        #endregion

        #region Constructors
        public UserProfilePermission()
        {
        }

        public UserProfilePermission(int userProfileId, int permissionId, int userIdChange)
        {
            PermissionId = permissionId;
            UserProfileId = userProfileId;
            UserIdChange = userIdChange;
        }
        #endregion

        #region Validators
        public override bool IsValid()
        {
            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
        #endregion

        #region Methods
        public CommandResult Update(int userProfileId, int permissionId, int userIdChange)
        {
            UserProfileId = userProfileId;
            PermissionId = permissionId;

            UserIdChange = userIdChange;
            return new CommandResult(true, "Permissão do usuário atualizada com sucesso");
        }
        #endregion

        #region Log
        public override string KeyValueLog()
        {
            throw new System.NotImplementedException();
        }
        #endregion

    }
}

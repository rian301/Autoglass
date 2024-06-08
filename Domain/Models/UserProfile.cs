using Autoglass.Domain.Core.Models;
using Autoglass.Domain.DTO;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Autoglass.Domain.Models
{
    public class UserProfile : EntityLog<UserProfile, int>
    {
        #region Properties
        public string Name { get; private set; }

        public virtual IList<UserProfilePermission> Permissions { get; set; }

        #endregion

        #region Constructors
        public UserProfile()
        {
        }

        public UserProfile(int userIdChange)
        {
            UserIdChange = userIdChange;
            Permissions = new List<UserProfilePermission>();
        }

        public UserProfile(string name, int userIdChange)
        {
            Name = name;
            UserIdChange = userIdChange;

            Permissions = new List<UserProfilePermission>();
        }
        #endregion

        #region Methods
        public CommandResult Update(string name)
        {
            Name = name;

            return new CommandResult(true);
        }

        #region Permission
        public CommandResult AddPermission(IList<UserProfilePermission> permissions, int idUserChange)
        {
            foreach (var permission in permissions)
            {
                var result = AddPermission(new UserProfilePermission(Id, permission.PermissionId, idUserChange));
                if (!result.Success)
                    return result;
            }

            return new CommandResult(true);
        }

        public CommandResult AddPermission(UserProfilePermission permission)
        {
            Permissions.Add(permission);
            return new CommandResult(true);
        }

        public CommandResult UpdateUserProfilePermissions(IList<DTOUserProfilePermission> dto, int idUserChange)
        {
            var permissionAdded = dto.Where(x => !Permissions.Any(y => y.PermissionId == x.PermissionId)).ToList();
            var permissionMaintained = dto.Where(x => Permissions.Any(y => y.PermissionId == x.PermissionId)).ToList();
            var permissionRemoved = Permissions.Where(x => !dto.Any(y => y.PermissionId == x.PermissionId)).ToList();

            foreach (var item in permissionAdded)
            {
                Permissions.Add(new UserProfilePermission(item.UserProfileId.Value, item.PermissionId.Value, idUserChange));
            }

            foreach (var item in permissionRemoved)
            {
                Permissions.Remove(item);
            }

            return new CommandResult(true, "Permissões Atualizadas");
        }
        #endregion

        #endregion

        #region Validadores
        public override bool IsValid()
        {
            RuleFor(c => c.Name)
                   .NotEmpty().WithMessage("Nome do perfil é obrigatorio")
                   .MaximumLength(100).WithMessage("Nome do perfil não pode ser superior a 100 caracteres");


            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
        #endregion
    }
}

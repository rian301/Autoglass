using Autoglass.Domain.Core.Models;

namespace Autoglass.Domain.Models
{
    public class Permission : Entity<Permission, int>
    {
        #region Properties
        public string ConstPermission { get; set; }
        public string Name { get; set; }
        #endregion

        #region Constructors
        public Permission()
        {
        }

        public Permission(string name, string constPermission)
        {
            Name = name;
            ConstPermission = constPermission;
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
        #endregion

    }
}

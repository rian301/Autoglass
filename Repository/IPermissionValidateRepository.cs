namespace Autoglass.Repository
{
    public interface IPermissionValidateRepository
    {
        bool ValidateUserPermission(int idUser, string[] constPermission);
        bool ValidateUserPermission(int idUser, string constPermission);

    }
}

namespace Autoglass.Application
{
    public interface IPermissionValidateApp
    {
        bool ValidateUserPermission(int idUser, string[] constPermission);
        bool ValidateUserPermission(int idUser, string constPermission);

    }
}

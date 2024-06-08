//using Autoglass.Infra.CrossCutting.Identity;
//using Autoglass.Infra.CrossCutting.Identity.Models.AccountViewModels;
//using Autoglass.Infra.CrossCutting.Identity.Models.ManageViewModels;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Autoglass.Service
//{
//    public interface IUserService
//    {
//        Task<TokenViewModel> GenerateToken(LoginViewModel model, dynamic tokenConfigurations);
//        Task<TokenViewModel> RefreshToken(LoginRefreshTokenViewModel model, dynamic tokenConfigurations);
//        Task<bool> Register(RegisterViewModel model, dynamic tokenConfigurations, string role);
//        Task<bool> Update(UserUpdateViewModel model, dynamic tokenConfigurations);
//        Task<bool> Update(User user);
//        Task<User> FindById(int id);
//        Task<User> FindByEmail(string email);
//        Task<User> FindByUserName(string username);
//        Task<bool> ChangePasswordAdmin(ChangePasswordAdminViewModel model);
//        Task<bool> ChangePassword(ChangePasswordViewModel model);
//        Task<bool> PasswordRecovery(string email, dynamic tokenConfigurations);
//        Task<bool> ResetPassword(ResetPasswordViewModel model);
//        IEnumerable<User> GetAll();
//        Task<bool> ChangeStatus(int id, bool status);
//        Task<bool> AddRoleInUserAsync(int id, string role);
//        Task<IList<User>> GetAllInRoleAsync(string role);
//        Task<string> GeneratePasswordResetTokenAsync(string id);
//        Task<bool> CheckEmailInUse(string email, int? ignoreId);
//    }
//}

//using Autoglass.Infra.CrossCutting.Identity;
//using Autoglass.Infra.CrossCutting.Identity.Models.AccountViewModels;
//using Autoglass.Infra.CrossCutting.Identity.Models.ManageViewModels;
//using Autoglass.Service;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Autoglass.Application.Implement
//{
//    public class UserApp : IUserApp
//    {

//        #region Properties
//        private readonly IUserService _service;
//        #endregion

//        #region Constructor
//        public UserApp(IUserService service) => _service = service;
//        #endregion

//        #region Methods
//        public Task<bool> ChangePasswordAdmin(ChangePasswordAdminViewModel model) => _service.ChangePasswordAdmin(model);
//        public Task<bool> ChangePassword(ChangePasswordViewModel model) => _service.ChangePassword(model);
//        public Task<bool> PasswordRecovery(string emailUser, dynamic tokenConfigurations) => _service.PasswordRecovery(emailUser, tokenConfigurations);
//        public Task<bool> ResetPassword(ResetPasswordViewModel model) => _service.ResetPassword(model);
//        public async Task<User> FindById(int id) => await _service.FindById(id);
//        public async Task<User> FindByEmail(string email) => await _service.FindByEmail(email);
//        public async Task<User> FindByUserName(string username) => await _service.FindByUserName(username);
//        public async Task<TokenViewModel> GenerateToken(LoginViewModel model, dynamic tokenConfigurations) => await _service.GenerateToken(model, tokenConfigurations);
//        public async Task<TokenViewModel> RefreshToken(LoginRefreshTokenViewModel model, dynamic tokenConfigurations) => await _service.RefreshToken(model, tokenConfigurations);

//        public async Task<bool> Register(RegisterViewModel model, dynamic tokenConfigurations, string role) => await _service.Register(model, tokenConfigurations, role);
//        public async Task<bool> Update(UserUpdateViewModel model, dynamic tokenConfigurations) => await _service.Update(model, tokenConfigurations);
//        public async Task<bool> Update(User user) => await _service.Update(user);

//        public IEnumerable<User> GetAll() => _service.GetAll();

//        public async Task<bool> ChangeStatus(int id, bool status) => await _service.ChangeStatus(id, status);

//        public Task<bool> AddRoleInUserAsync(int id, string role) => _service.AddRoleInUserAsync(id, role);
//        public Task<IList<User>> GetAllInRoleAsync(string role) => _service.GetAllInRoleAsync(role);
//        public Task<string> GeneratePasswordResetTokenAsync(string id) => _service.GeneratePasswordResetTokenAsync(id);
//        public Task<bool> CheckEmailInUse(string email, int? ignoreId) => _service.CheckEmailInUse(email, ignoreId);
//        #endregion
//    }
//}

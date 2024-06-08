//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.WebUtilities;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using Newtonsoft.Json;
//using Autoglass.Domain.Core.Consts;
//using Autoglass.Domain.Core.Notifications;
//using Autoglass.Domain.Interfaces;
//using Autoglass.Domain.Models;
//using Autoglass.Infra.CrossCutting.Common.Seguranca;
//using Autoglass.Infra.CrossCutting.Identity;
//using Autoglass.Infra.CrossCutting.Identity.Models;
//using Autoglass.Infra.CrossCutting.Identity.Models.AccountViewModels;
//using Autoglass.Infra.CrossCutting.Identity.Models.ManageViewModels;
//using Autoglass.Infra.CrossCutting.Identity.Services;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Security.Principal;
//using System.Text;
//using System.Threading.Tasks;
//using System.Transactions;
//using Autoglass.Service;

//namespace Procard.Service.Implement
//{
//    public class UserService : IUserService
//    {
//        #region Properties
//        private readonly UserManager<User> _userManager;
//        private readonly RoleManager<Role> _roleManager;
//        private readonly SignInManager<User> _signInManager;
//        private readonly IConfiguration _configuration;
//        private readonly IEmailSender _emailSender;
//        private readonly ILoginRefreshTokenService _loginRefreshTokenService;
//        private readonly IUser _user;
//        private readonly INotificationHandler<DomainNotification> _notification;
//        #endregion

//        #region Constructor
//        public UserService(IConfiguration configuration, INotificationHandler<DomainNotification> notification, UserManager<User> userManager, SignInManager<User> signInManager, IUser user, RoleManager<Role> roleManager,
//            ILoginRefreshTokenService loginRefreshTokenService, IEmailSender emailSender)
//        {
//            _roleManager = roleManager;
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _configuration = configuration;
//            _user = user;
//            _loginRefreshTokenService = loginRefreshTokenService;
//            _notification = notification;
//            _emailSender = emailSender;
//        }
//        #endregion

//        #region Methods
//        public async Task<bool> ResetPassword(ResetPasswordViewModel model)
//        {

//            var user = await _userManager.FindByEmailAsync(model.Email);
//            if (user == null)
//            {
//                _notification.Handle(new DomainNotification("UserInvalid", "Usuário Inválido"));
//                return false;
//            }

//            var codeDecodedBytes = WebEncoders.Base64UrlDecode(model.Code);
//            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);

//            var result = await _userManager.ResetPasswordAsync(user, codeDecoded, model.Password);

//            return result.Succeeded;
//        }

//        public async Task<bool> ChangePasswordAdmin(ChangePasswordAdminViewModel model)
//        {
//            if (!_user.Id.HasValue)
//            {
//                _notification.Handle(new DomainNotification("UserNotFound", "Usuário não encontrado"));
//                return false;
//            }


//            var user = await _userManager.FindByIdAsync(model.UserId);
//            if (user == null)
//            {
//                _notification.Handle(new DomainNotification("UserInvalid", "Usuário Inválido"));
//                return false;
//            }

//            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
//            var result = await _userManager.ResetPasswordAsync(user, code, model.NewPassword);

//            return result.Succeeded;
//        }

//        public async Task<bool> ChangePassword(ChangePasswordViewModel model)
//        {
//            var user = await _userManager.FindByIdAsync(_user.Id.Value.ToString());
//            if (user == null)
//            {
//                _notification.Handle(new DomainNotification("UserInvalid", "Usuário Inválido"));
//                return false;
//            }

//            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

//            return result.Succeeded;
//        }

//        public async Task<bool> PasswordRecovery(string email, dynamic tokenConfigurations)
//        {
//            var user = await _userManager.FindByEmailAsync(email);

//            if (user != null)
//            {
//                var roles = await _userManager.GetRolesAsync(user);
//                if (!ValidateAudienceApp(roles, tokenConfigurations))
//                    return false;

//                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

//                if (code != null)
//                {
//                    await _emailSender.SendEmailRecoveryPasswordAsync(email, user.Id, code);
//                    return true;
//                }
//            }

//            return false;
//        }

//        public async Task<string> GeneratePasswordResetTokenAsync(string id)
//        {
//            var user = await _userManager.FindByIdAsync(id);

//            if (user != null)
//                return await _userManager.GeneratePasswordResetTokenAsync(user);

//            return null;
//        }

//        public async Task<User> FindById(int id)
//        {
//            var usuario = await _userManager.FindByIdAsync(id.ToString());

//            //usuario.Imagem = !String.IsNullOrWhiteSpace(usuario.Imagem) ? $@"{UploadUtils.GetPath(_configuration, TipoUploadEnum.Usuario, false)}/{usuario.Imagem}" : null;

//            return usuario;
//        }

//        public async Task<User> FindByEmail(string email)
//        {
//            return await _userManager.FindByEmailAsync(email);
//        }

//        public async Task<User> FindByUserName(string username)
//        {
//            return await _userManager.FindByNameAsync(username);
//        }

//        public async Task<TokenViewModel> GenerateToken(LoginViewModel model, dynamic tokenConfigurations)
//        {
//            var user = await _userManager.FindByEmailAsync(model.Email);

//            if (user == null || !user.Active)
//            {
//                _notification.Handle(new DomainNotification("UserInvalid", "Usuário ou senha inválido"));
//                return null;
//            }

//            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
//            if (result.Succeeded)
//            {
//                var roles = await _userManager.GetRolesAsync(user);
//                if (!ValidateAudienceApp(roles, tokenConfigurations))
//                    _notification.Handle(new DomainNotification("UserInvalid", "Usuário ou senha inválido"));
//                else
//                    return await GerarTokenAsync(user, roles, tokenConfigurations);
//            }
//            else
//            {
//                _notification.Handle(new DomainNotification("UserInvalid", "Usuário ou senha inválido"));
//            }

//            return null;
//        }

//        public async Task<TokenViewModel> RefreshToken(LoginRefreshTokenViewModel model, dynamic tokenConfigurations)
//        {
//            var refreshToken = _loginRefreshTokenService.GetByUserToken(model.IdUser, Encoding.GetEncoding("ISO-8859-1").GetString(Convert.FromBase64String(model.Code)));

//            if (refreshToken == null)
//            {
//                _notification.Handle(new DomainNotification("ModelInvalid", "Requisição inválida"));
//                return null;
//            }

//            var token = JsonConvert.DeserializeObject<TokenViewModel>(Encoding.GetEncoding("ISO-8859-1").GetString(Convert.FromBase64String(refreshToken.Token)));

//            var user = await _userManager.FindByIdAsync(token.user_id.ToString());

//            if (user == null || !user.Active)
//            {
//                _notification.Handle(new DomainNotification("UserInvalid", "Usuário ou senha inválido"));
//                return null;
//            }

//            var roles = await _userManager.GetRolesAsync(user);
//            if (!ValidateAudienceApp(roles, tokenConfigurations))
//            {
//                _notification.Handle(new DomainNotification("UserInvalid", "Usuário ou senha inválido"));
//                return null;
//            }

//            return GerarTokenAsync(user, roles, tokenConfigurations);
//        }

//        public async Task<bool> Register(RegisterViewModel model, dynamic tokenConfigurations, string role)
//        {
//            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
//            {
//                var user = await _userManager.FindByEmailAsync(model.Email);
//                if (user != null)
//                {
//                    _notification.Handle(new DomainNotification("UserInvalid", "O e-mail informado está indisponível."));
//                    return false;
//                }

//                user = new User(model.Name, model.Email, model.Email, model.UserProfileId);

//                var result = await _userManager.CreateAsync(user);

//                if (!result.Succeeded)
//                {
//                    _notification.Handle(new DomainNotification("UserInvalid", "O e-mail informado está indisponível."));
//                    return false;
//                }

//                if (!string.IsNullOrEmpty(role))
//                    await _userManager.AddToRoleAsync(user, role);

//                if (!string.IsNullOrEmpty(model.Password))
//                {
//                    var resultSenha = await _userManager.AddPasswordAsync(user, model.Password);
//                    if (!resultSenha.Succeeded)
//                    {
//                        scope.Dispose();
//                        _notification.Handle(new DomainNotification("UserPasswordInvalid", "Não foi possível cadastrar a senha informada, verifique a senha e tente novamente."));
//                        return false;
//                    }
//                    // HABILITAR SE É PARA CRIAR O USUÁRIO E JÁ CONFIRMAR O E-MAIL AUTOMATICAMENTE
//                    else
//                    {
//                        //await _emailSender.SendEmailWelcome(user);
//                        TokenViewModel token = await GenerateToken(new LoginViewModel { Email = model.Email, Password = model.Password }, tokenConfigurations);
//                        await _userManager.ConfirmEmailAsync(user, token.access_token);
//                    }
//                }

//                scope.Complete();

//                return result.Succeeded;
//            }
//        }

//        public async Task<bool> Update(UserUpdateViewModel model, dynamic tokenConfigurations)
//        {
//            var user = await _userManager.FindByIdAsync(model.Id.ToString());

//            if (user != null)
//            {
//                //if (CheckCPFInUse(model.CPF, user.Id))
//                //    throw new Exception("O CPF informado já está em uso");

//                user.Update(model.Name, model.Email, model.Active, model.UserProfileId);

//                var result = await _userManager.UpdateAsync(user);

//                if (!result.Succeeded)
//                    _notification.Handle(new DomainNotification("EmailInvalid", "O e-mail informado está indisponível."));

//                return result.Succeeded;
//            }

//            return false;
//        }

//        public async Task<bool> Update(User user)
//        {
//            if (user != null)
//            {
//                var result = await _userManager.UpdateAsync(user);
//                return result.Succeeded;
//            }
//            return false;
//        }


//        public async Task<bool> ChangeStatus(int id, bool status)
//        {
//            var user = await _userManager.FindByIdAsync(id.ToString());
//            if (user == null)
//            {
//                _notification.Handle(new DomainNotification("UserInvalid", "Usuário inválido"));
//                return false;
//            }

//            user.SetStatus(status);

//            var result = await _userManager.UpdateAsync(user);

//            return result.Succeeded;
//        }

//        private async Task<TokenViewModel> GerarTokenAsync(User user, IList<string> roles, dynamic tokenConfigurations)
//        {
//            List<Claim> claims = new List<Claim> {
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
//                new Claim(JwtRegisteredClaimNames.Email, user.Email.ToString()),
//                new Claim(CustomClaimTypes.ID_USER, user.Id.ToString()),
//                new Claim(ClaimTypes.Name, user.Name)
//            };

//            // Add Customer Claim
//            //var customer = await _customerService.GetByUserIdAsync(user.Id);
//            //if (customer?.Id > 0)
//            //    claims.Add(new Claim(CustomClaimTypes.ID_CUSTOMER, customer.Id.ToString()));

//            // Add Roles in Claims
//            if (roles.Count > 0)
//                claims.AddRange(roles.Select(s => new Claim(ClaimTypes.Role, s)).ToList());

//            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user.UserName.ToString(), ClaimTypes.NameIdentifier), claims);
//            DateTime dataCriacao = DateTime.Now;
//            DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Key));
//            var signingConfigurations = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var handler = new JwtSecurityTokenHandler();
//            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
//            {
//                Issuer = tokenConfigurations.Issuer,
//                Audience = tokenConfigurations.Audience,
//                SigningCredentials = signingConfigurations,
//                Subject = identity,
//                NotBefore = dataCriacao,
//                Expires = dataExpiracao
//            });

//            var token = handler.WriteToken(securityToken);
//            var refreshTokenId = Encrypt.GetHash(Guid.NewGuid().ToString("n"));

//            var tokenVM = new TokenViewModel()
//            {
//                created = dataCriacao,
//                expiration = dataExpiracao,
//                access_token = token,
//                name = user.Name,
//                user_id = user.Id,
//                refresh_token = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(refreshTokenId)),
//                register_completed = roles.Any(a => a.Equals(RolesConst.SystemApp))
//            };

//            var refreshtoken = new LoginRefreshToken(
//                refreshTokenId,
//                user.Id,
//                Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(JsonConvert.SerializeObject(tokenVM))),
//                DateTime.UtcNow.AddSeconds(tokenConfigurations.Seconds));

//            _loginRefreshTokenService.Add(refreshtoken);

//            return tokenVM;
//        }

//        private bool ValidateAudienceApp(IList<string> roles, dynamic tokenConfigurations)
//        {
//            List<string> audRoles = new List<string>();

//            if (tokenConfigurations.Audience == AudienceAppConst.System)
//            {
//                audRoles.Add(RolesConst.SystemApp);
//            }

//            return roles.Any(a => audRoles.Contains(a));
//        }

//        public IEnumerable<User> GetAll()
//        {
//            return _userManager.Users.OrderBy(o => o.Name).ToList();
//        }

//        public Task<IList<User>> GetAllInRoleAsync(string role)
//        {
//            return _userManager.GetUsersInRoleAsync(role);
//        }

//        public async Task<bool> AddRoleInUserAsync(int id, string role)
//        {
//            var user = await FindById(id);
//            if (user == null)
//                return false;

//            await _userManager.AddToRoleAsync(user, role);

//            return true;
//        }

//        public async Task<bool> CheckEmailInUse(string email, int? ignoreId)
//        {
//            var user = await _userManager.FindByEmailAsync(email);
//            return (user == null || user.Id == ignoreId) ? false : true;
//        }
//        #endregion
//    }
//}

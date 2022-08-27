using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.ViewModels.Roles;
using RealStateApp.Core.Application.ViewModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface IUserServices
    {
        Task<List<UsersViewModel>> GetAllUsersAsync();
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsync(SaveUsersViewModel vm, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm);
        Task SignOutAsync();
        Task<List<RolesViewModel>> GetAllRoles();
        Task<RolesViewModel> GetRolByName(string RolName);
        Task<List<UsersViewModel>> GetUserByRol(string RolName);
        Task<UsersViewModel> GetUserById(string Id);
        Task Delete(string Id);
        Task<RegisterResponse> UpdateAsycn(SaveClientAgentViewModel vm, string Id)
    }
}
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.ViewModels.Roles;
using RealStateApp.Core.Application.ViewModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<List<UsersViewModel>> GetUsersAsync();
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin);
        Task<RegisterResponse> UpdateUser(RegisterRequest request, string Id);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task SignOutAsync();
        Task<List<RolesViewModel>> GetAllRoles();
        Task<RolesViewModel> GetRolByName(string roleName);
        Task<List<UsersViewModel>> GetUserByRol(string roleName);
        Task<UsersViewModel> GetUserByIdAsync(string Id);
        Task Delete(string id);
        Task Active(string id);
        Task Inactive(string id);
        Task<UsersStateByRolViewModel> GetAllUserStateByRol(string Rol);
    }
}
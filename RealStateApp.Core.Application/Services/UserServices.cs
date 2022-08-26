using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.Roles;
using RealStateApp.Core.Application.ViewModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse userViewModel;

        public UserServices(IAccountService accountService, IMapper mapper,IHttpContextAccessor httpContextAccesso)
        {
            _accountService = accountService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccesso;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
            return userResponse;
        }
        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        public async Task<List<UsersViewModel>> GetAllUsersAsync()
        {
            var lista = await _accountService.GetUsersAsync();
            return lista;
        }
        public async Task<RegisterResponse> RegisterAsync(SaveUsersViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterBasicUserAsync(registerRequest, origin);
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin)
        {
            ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(vm);
            return await _accountService.ForgotPasswordAsync(forgotRequest, origin);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm)
        {
            ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(vm);
            return await _accountService.ResetPasswordAsync(resetRequest);
        }

        public async Task<List<RolesViewModel>> GetAllRoles()
        {
            return await _accountService.GetAllRoles();
        }

        public async Task<RolesViewModel> GetRolByName(string RolName)
        {
            return await _accountService.GetRolByName(RolName);
        }

        public async Task<List<UsersViewModel>> GetUserByRol(string RolName)
        {
            return await _accountService.GetUserByRol(RolName);
        }

        public async Task<UsersViewModel> GetUserById()
        {
            return await _accountService.GetUserByIdAsync(userViewModel.Id);
        }
        public async Task  Delete(string Id)
        {
            await _accountService.Delete(Id);
        }
    }
}

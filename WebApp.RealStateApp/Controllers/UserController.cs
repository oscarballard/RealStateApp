using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.ViewModels.User;
using System.Threading.Tasks;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.Dtos.Account;
using WebApp.RealStateApp.Middlewares;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.ViewModels.Roles;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using AutoMapper;
using System;

namespace WebApp.RealStateApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserServices _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserServices userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _userService.LoginAsync(vm);
            if (userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                vm.HasError = userVm.HasError;
                vm.Error = userVm.Error;
                return View(vm);
            }
        }

        
        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Users", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpGet]
        public async Task<IActionResult> RegisterUser()
        {
            SaveClientAgentViewModel vm = new();
            List<RolesViewModel> RolesList = new();

            RolesViewModel rolesVm = await _userService.GetRolByName("Client");
            RolesList.Add(rolesVm);
            rolesVm = await _userService.GetRolByName("Agent");
            RolesList.Add(rolesVm);
            vm.RolesList = RolesList;

            return View(vm);
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> RegisterUser(SaveClientAgentViewModel vm)
        {
            SaveUsersViewModel vmm = _mapper.Map<SaveUsersViewModel>(vm);

            if (!ModelState.IsValid)
            {
                List<RolesViewModel> RolesList = new();
                RolesViewModel rolesVm = await _userService.GetRolByName("Client");
                RolesList.Add(rolesVm);
                rolesVm = await _userService.GetRolByName("Agent");
                RolesList.Add(rolesVm);
                vm.RolesList = RolesList;
                return View(vm);
            }

            var origin = Request.Headers["origin"];
            RegisterResponse response = await _userService.RegisterAsync(vmm, origin);
            if (response.HasError)
            {
                List<RolesViewModel> RolesList = new();
                RolesViewModel rolesVm = await _userService.GetRolByName("Client");
                RolesList.Add(rolesVm);
                rolesVm = await _userService.GetRolByName("Agent");
                RolesList.Add(rolesVm);
                vm.RolesList = RolesList;
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var origin = Request.Headers["origin"];
            ForgotPasswordResponse response = await _userService.ForgotPasswordAsync(vm, origin);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "Users", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ResetPassword(string token)
        {
            return View(new ResetPasswordViewModel { Token = token });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            
            ResetPasswordResponse response = await _userService.ResetPasswordAsync(vm);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "Users", action = "Index" });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}


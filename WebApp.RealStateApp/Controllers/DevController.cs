using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.RealStateApp.Middlewares;

namespace WebApp.RealStateApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DevController : Controller
    {
        private readonly IUserServices _userServices;

        public DevController(IUserServices userService)
        {
            _userServices = userService;
        }

        public async Task<IActionResult> Index()
        {
            SaveUsersViewModel vm = new();
            vm.Roles = await _userServices.GetRolByName(Roles.Dev.ToString());
            ViewBag.admins = await _userServices.GetUserByRol(Roles.Dev.ToString());
            return View("Index", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUsersViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.admins = await _userServices.GetUserByRol(Roles.Dev.ToString());
                vm.Roles = await _userServices.GetRolByName(Roles.Dev.ToString());
                return View("Index",vm);
            }
            var origin = Request.Headers["origin"];
            RegisterResponse response = await _userServices.RegisterAsync(vm, origin);
            if (response.HasError)
            {
                ViewBag.admins = await _userServices.GetUserByRol(Roles.Dev.ToString());
                vm.Roles = await _userServices.GetRolByName(Roles.Dev.ToString());
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View("Index", vm);
            }
            return RedirectToRoute(new { controller = "Dev", action = "Index" });
        }
    }
}

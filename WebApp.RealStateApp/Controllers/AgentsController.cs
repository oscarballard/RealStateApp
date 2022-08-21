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
    public class AgentsController : Controller
    {
        private readonly IUserServices _userServices;

        public AgentsController(IUserServices userService)
        {
            _userServices = userService;
        }

        public async Task<IActionResult> Index()
        {
            SaveUsersViewModel vm = new();
            vm.Roles = await _userServices.GetRolByName(Roles.Agent.ToString());
            ViewBag.admins = await _userServices.GetUserByRol(Roles.Agent.ToString());
            return View("Index", vm);
        }
    }
}

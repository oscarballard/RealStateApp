using AutoMapper;
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
    public class AdminsController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public AdminsController(IUserServices userService, IMapper mapper)
        {
            _userServices = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            SaveUsersViewModel vm = new();
            vm.Roles = await _userServices.GetRolByName(Roles.Admin.ToString());
            ViewBag.admins = await _userServices.GetUserByRol(Roles.Admin.ToString());
            return View("Index", vm);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            SaveUsersViewModel vm = new();
            UsersViewModel vmm = await _userServices.GetUserById(Id);
            vm = _mapper.Map<SaveUsersViewModel>(vmm);
            vm.Roles = await _userServices.GetRolByName(Roles.Admin.ToString());
            ViewBag.admins = await _userServices.GetUserByRol(Roles.Admin.ToString());
            return View("Index", vm);
        }

        //[ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Register(SaveUsersViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.admins = await _userServices.GetUserByRol(Roles.Admin.ToString());
                vm.Roles = await _userServices.GetRolByName(Roles.Admin.ToString());
                return View("Index",vm);
            }
            var origin = Request.Headers["origin"];
            RegisterResponse response = await _userServices.RegisterAsync(vm, origin);
            if (response.HasError)
            {
                ViewBag.admins = await _userServices.GetUserByRol(Roles.Admin.ToString());
                vm.Roles = await _userServices.GetRolByName(Roles.Admin.ToString());
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View("Index", vm);
            }
            return RedirectToRoute(new { controller = "Admins", action = "Index" });
        }
    }
}

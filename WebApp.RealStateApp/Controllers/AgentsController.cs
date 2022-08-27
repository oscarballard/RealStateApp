using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Application.ViewModels.Roles;
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
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AgentsController(IUserServices userService, IMapper mapper, IHttpContextAccessor httpContextAccesso)
        {
            _userServices = userService;
            _userServices = userService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccesso;
        }

        public async Task<IActionResult> Index()
        {
            SaveUsersViewModel vm = new();
            vm.Roles = await _userServices.GetRolByName(Roles.Agent.ToString());
            ViewBag.admins = await _userServices.GetUserByRol(Roles.Agent.ToString());
            return View("Index", vm);
        }

        public async Task<IActionResult> AgentList(FilterUserViewModel filter)
        {
            List<UsersViewModel> vm = new();
            vm = await _userServices.GetUserByRol(Roles.Agent.ToString());
            if (filter.firstName != null)
            {
                vm = vm.Where(u => u.FirstName.ToLower().Contains(filter.firstName.ToLower())).ToList();
                vm = vm.Where(u => u.IsActive == true).ToList(); 
                ViewBag.Filters = filter;
            }
            return View(vm);
        }

        //public async Task<IActionResult> MiPerfil()
        //{
        //    UsersViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        //    SaveClientAgentViewModel vm = new();
        //    vm = _mapper.Map<SaveClientAgentViewModel>(await _userServices.GetUserById(userViewModel.Id));
        //    List<RolesViewModel> RolesList = new();
        //    return View(vm);
        //}

        public async Task<IActionResult> Active(string Id)
        {
            SaveClientAgentViewModel vm = new();
            vm = _mapper.Map<SaveClientAgentViewModel>(await _userServices.GetUserById(Id));
            vm.IsActive = true;
            await _userServices.UpdateAsycn(vm, Id);
            vm.Roles = await _userServices.GetRolByName(Roles.Agent.ToString());
            ViewBag.admins = await _userServices.GetUserByRol(Roles.Agent.ToString());
            return RedirectToRoute(new { controller = "Agents", action = "Index" });
        }

        public async Task<IActionResult> Inactivate(string Id)
        {
            SaveClientAgentViewModel vm = new();
            vm = _mapper.Map<SaveClientAgentViewModel>(await _userServices.GetUserById(Id));
            vm.IsActive = false;
            await _userServices.UpdateAsycn(vm, Id);
            vm.Roles = await _userServices.GetRolByName(Roles.Agent.ToString());
            ViewBag.admins = await _userServices.GetUserByRol(Roles.Agent.ToString());
            return RedirectToRoute(new { controller = "Agents", action = "Index" });
        }

        //public async Task<IActionResult> Delete()
        //{

        //}

    }
}

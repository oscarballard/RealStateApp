using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Interfaces.Services;
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

        public AgentsController(IUserServices userService, IMapper mapper)
        {
            _userServices = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            SaveUsersViewModel vm = new();
            vm.Roles = await _userServices.GetRolByName(Roles.Agent.ToString());
            ViewBag.admins = await _userServices.GetUserByRol(Roles.Agent.ToString());
            return View("Index", vm);
        }

        public async Task<IActionResult> MiPerfil()
        {
            SaveClientAgentViewModel vm = new();
            vm = _mapper.Map<SaveClientAgentViewModel>(await _userServices.GetUserById());
            List<RolesViewModel> RolesList = new();
            return View(vm);
        }

        //public async Task<IActionResult> Active()
        //{

        //}

        //public async Task<IActionResult> inactivate()
        //{

        //}

        //public async Task<IActionResult> Delete()
        //{

        //}

    }
}

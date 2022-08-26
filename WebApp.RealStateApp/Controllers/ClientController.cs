using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.ClientLike;
using RealStateApp.Core.Application.ViewModels.Roles;
using RealStateApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.RealStateApp.Middlewares;

namespace WebApp.RealStateApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientLikeService _clientServices;
        private readonly IMapper _mapper;

        public ClientController(IClientLikeService clientServices, IMapper mapper)
        {
            _clientServices = clientServices;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<int> ManageLike(int Id)
        {
            List<ClientLikeViewModel> vm = await _clientServices.GetByPropertyAndClient(Id);
            if (vm.Count > 0)
            {
                await this.Dislike(vm);
                return 1;
            }
            else
            {
                await this.Like(Id);
                return 2;
            }
            return 1;
        }

        public async Task Dislike(List<ClientLikeViewModel> vm)
        {
            await _clientServices.Dislike(vm);
        }

        public async Task Like(int Id)
        {
            await _clientServices.Add(Id);
        }


    }
}

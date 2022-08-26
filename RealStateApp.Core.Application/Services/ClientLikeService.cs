using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.ClientLike;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Application.ViewModels.PropertyImprovements;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services
{
    public class ClientLikeService : GenericService<SaveClientLikeViewModel, ClientLikeViewModel, ClientLike>, IClientLikeService
    {
        private readonly IClientLikeRepository _clientLikeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsersViewModel userViewModel;

        public ClientLikeService(IClientLikeRepository clientLikeRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(clientLikeRepository, mapper)
        {
            _clientLikeRepository = clientLikeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UsersViewModel>("user");
        }

        public async Task<List<ClientLikeViewModel>> GetByPropertyAndClient(int Id) 
        {
            var LikeList = await _clientLikeRepository.GetAllAsync();
            List<ClientLikeViewModel> LikeListModel = LikeList.Select(l => new ClientLikeViewModel
            {

                Id = l.Id,
                IdPropiedad = l.IdPropiedad,
                IdClient = l.IdClient
            }).ToList();

            LikeListModel = LikeListModel.Where(l => l.IdPropiedad == Id).ToList();
            LikeListModel = LikeListModel.Where(l => l.IdClient == userViewModel.Id).ToList();

            return LikeListModel;
        }

        public async Task<SaveClientLikeViewModel> Add(int IdPropiedad)
        {
            SaveClientLikeViewModel vm = new();
            vm.IdPropiedad  = IdPropiedad;
            vm.IdClient       = userViewModel.Id;
            SaveClientLikeViewModel UserVm = await base.Add(vm);
            return UserVm;
        }

        public async Task Dislike(List<ClientLikeViewModel> Likes)
        {
            foreach (ClientLikeViewModel fl in Likes)
            {
                await base.Delete(fl.Id);
            }
        }
    }
}

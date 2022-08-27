using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.ClientLike;
using RealStateApp.Core.Application.ViewModels.Improvements;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.SalesType;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services
{
    public class PropertyService : GenericService<SavePropertyViewModel, PropertyViewModel,Property>, IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;
        private readonly IClientLikeService _clientLikeService;
        private readonly UsersViewModel userViewModel;
        public PropertyService(IAccountService accountService,IPropertyRepository propertyRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IClientLikeService clientLikeService) : base(propertyRepository , mapper)
        {
            _propertyRepository = propertyRepository;
            _clientLikeService = clientLikeService;
            _mapper = mapper;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UsersViewModel>("user");
        }
        public async Task<PropertyViewModel> GetByIdViewModel(int Id)
        {
            var entity = await _propertyRepository.GetByIdWithIncludeAsync(Id);
            PropertyViewModel vm = _mapper.Map<PropertyViewModel>(entity);
            vm.Usuario = await _accountService.GetUserByIdAsync(vm.IdAgente);
         
            return vm;
        }

        public override async Task<SavePropertyViewModel> GetByIdSaveViewModel(int Id)
        {
            var entity = await _propertyRepository.GetByIdWithIncludeAsync(Id);
            SavePropertyViewModel vm = _mapper.Map<SavePropertyViewModel>(entity);

            return vm;
        }

        public async Task<PropertyViewModel> GetByCodeViewModel(string Code)
        {
            var entity = await _propertyRepository.GetByCodeWithIncludeAsync(Code);
            PropertyViewModel vm = _mapper.Map<PropertyViewModel>(entity);

            return vm;
        }

        public async Task<List<PropertyViewModel>> GetAllViewModelWithFilters(FilterPropertyViewModel filters)
        {
            var productList = await _propertyRepository.GetAllWithIncludeAsync(new List<string> { "TipoVenta", "TipoPropiedad"});

            var listViewModels = productList.Select(prop => new PropertyViewModel
            {
                Id = prop.Id,
                Codigo = prop.Codigo,
                TipoPropiedad = _mapper.Map<PropertyTypeViewModel>(prop.TipoPropiedad),
                TipoVenta = _mapper.Map<SalesTypeViewModel>(prop.TipoVenta),
                Descripcion = prop.Descripcion,
                CantHabitaciones = prop.CantHabitaciones,
                CantLavabos = prop.CantLavabos,
                Terreno = prop.Terreno,
                Imagen1 = prop.Imagen1,
                Imagen2 = prop.Imagen2,
                Imagen3 = prop.Imagen3,
                Imagen4 = prop.Imagen4,
                IdTipoPropiedad = prop.TipoPropiedad.Id,
                Precio = prop.Precio,
                IdAgente = prop.IdAgente
            }).ToList();


            if (filters.Tipo != null)
            {
                listViewModels = listViewModels.Where(product => product.IdTipoPropiedad == filters.Tipo.Value).ToList();
            }
            if (filters.CantHabitaciones != null)
            {
                listViewModels = listViewModels.Where(product => product.CantHabitaciones == filters.CantHabitaciones.Value).ToList();
            }

            if (filters.CantLavabos != null)
            {
                listViewModels = listViewModels.Where(product => product.CantLavabos == filters.CantLavabos.Value).ToList();
            }

            if (filters.MinPrecio != null)
            {
                listViewModels = listViewModels.Where(product => product.Precio >= filters.MinPrecio.Value).ToList();
            }

            if (filters.MaxPrecio != null)
            {
                listViewModels = listViewModels.Where(product => product.Precio <= filters.MaxPrecio.Value).ToList();
            }

            if (filters.IdAgent != null)
            {
                listViewModels = listViewModels.Where(product => product.IdAgente == filters.IdAgent.ToString()).ToList();
            }

            if (userViewModel != null)
            {
                List<PropertyViewModel> ListLikeWithLike = new();
                ClientLikeViewModel likes = new();
                foreach (PropertyViewModel p in listViewModels)
                {
                    if (filters.IdClient != null)
                    {
                        p.ClientLikes = await _clientLikeService.GetByPropertyAndClient(p.Id);
                        if (p.ClientLikes.Count > 0)
                        {
                            ListLikeWithLike.Add(p);
                        }
                    }
                    else
                    {
                        p.ClientLikes = await _clientLikeService.GetByPropertyAndClient(p.Id);
                        ListLikeWithLike.Add(p);
                    }
                }



                return ListLikeWithLike;
            }

            return listViewModels;
        }
    }
}

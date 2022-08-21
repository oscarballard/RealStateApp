using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
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
        private readonly UserViewModel userViewModel;
        public PropertyService(IPropertyRepository propertyRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(propertyRepository , mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }
        public async Task<List<PropertyViewModel>> GetAllViewModelWithFilters(FilterPropertyViewModel filters)
        {
            var productList = await _propertyRepository.GetAllWithIncludeAsync(new List<string> { "TipoVenta", "TipoPropiedad"});

            var listViewModels = productList.Select(prop => new PropertyViewModel
            {
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
                Imagen4 = prop.Imagen4
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

            return listViewModels;
        }
    }
}

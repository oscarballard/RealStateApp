using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Features.Properties.Queris;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.Improvements;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.SalesType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Properties.Queries.GetAllProperties
{
    public class GetAllPropertiesQuery : IRequest<ICollection<GetAllPropertiesResponse>>
    {
 
    }

    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, ICollection<GetAllPropertiesResponse>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        public GetAllPropertiesQueryHandler(IAccountService accountService, IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task<ICollection<GetAllPropertiesResponse>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            var propertiesList = await GetAllViewModelWithFilters();
            if (propertiesList == null || propertiesList.Count == 0) throw new Exception("No hay propiedades");

            return propertiesList;
        }

        private async Task<ICollection<GetAllPropertiesResponse>> GetAllViewModelWithFilters()
        {
            var productList = await _propertyRepository.GetAllWithIncludeAsync(new List<string> { "Mejoras"});
            
            var listViewModels = productList.Select(prop => new GetAllPropertiesResponse
            {
                Id = prop.Id,
                Codigo = prop.Codigo,
                IdTipoPropiedad = prop.IdTipoPropiedad,
                IdTipoVenta = prop.IdTipoVenta,
                Precio = prop.Precio,
                Terreno = prop.Terreno,
                CantHabitaciones = prop.CantHabitaciones,
                CantLavabos = prop.CantLavabos,
                Descripcion = prop.Descripcion,
                Mejoras = _mapper.Map<ICollection<ImprovementsViewModel>>(prop.Mejoras),
                IdAgente =prop.IdAgente
            }).ToList();

            foreach (var propiedad in listViewModels)
            {
                var agente = await _accountService.GetUserByIdAsync(propiedad.IdAgente);
                propiedad.NombreAgente = agente.FirstName;
            }
        
            return listViewModels;
        }
    }
}

using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Features.Properties.Queries.GetAllProperties;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Application.ViewModels.Improvements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Properties.Queries.GetPropertyById
{
    public class GetPropertyByIdQuery : IRequest<GetAllPropertiesResponse>
    {
        public int Id { get; set; }
    }
    public class GetAllPropertyQueryHandler : IRequestHandler<GetPropertyByIdQuery, GetAllPropertiesResponse>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public GetAllPropertyQueryHandler(IAccountService accountService, IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task<GetAllPropertiesResponse> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await GetByIdViewModel(request.Id);
            if (property == null) throw new Exception("Propiedad no encontrada");
            return property;
        }

        private async Task<GetAllPropertiesResponse> GetByIdViewModel(int Id)
        {
            var entity = await _propertyRepository.GetByIdWithIncludeAsync(Id);
            var agente = await _accountService.GetUserByIdAsync(entity.IdAgente);
            GetAllPropertiesResponse vm = new() {
                Id = entity.Id,
                Codigo = entity.Codigo,
                IdTipoPropiedad = entity.IdTipoPropiedad,
                IdTipoVenta = entity.IdTipoVenta,
                Precio = entity.Precio,
                Terreno = entity.Terreno,
                CantHabitaciones = entity.CantHabitaciones,
                CantLavabos = entity.CantLavabos,
                Descripcion = entity.Descripcion,
                Mejoras = _mapper.Map<ICollection<ImprovementsViewModel>>(entity.Mejoras),
                IdAgente = entity.IdAgente,
                NombreAgente = agente.FirstName
            };

            return vm;
        }
    }
}

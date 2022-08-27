using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.PropertyType.Queries.GetAllPropertyType
{
    public class GetAllPropertyTypeQuery : IRequest<ICollection<PropertyTypeViewModel>>
    {

    }

    public class GetAllPropertyTypeQueryHandler : IRequestHandler<GetAllPropertyTypeQuery, ICollection<PropertyTypeViewModel>>
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;
        public GetAllPropertyTypeQueryHandler(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<PropertyTypeViewModel>> Handle(GetAllPropertyTypeQuery request, CancellationToken cancellationToken)
        {
            var propertyTypeList = await GetAllViewModelWithFilters();
            if (propertyTypeList == null || propertyTypeList.Count == 0) throw new Exception("No hay Tipos De Ventas");

            return propertyTypeList;
        }

        private async Task<ICollection<PropertyTypeViewModel>> GetAllViewModelWithFilters()
        {
            var propertyTypeList = await _propertyTypeRepository.GetAllAsync();

            var listViewModels = propertyTypeList.Select(prop => new PropertyTypeViewModel
            {
                Id = prop.Id,
                Nombre = prop.Nombre,
                Descripcion = prop.Descripcion
            }).ToList();

            return listViewModels;
        }
    }
}

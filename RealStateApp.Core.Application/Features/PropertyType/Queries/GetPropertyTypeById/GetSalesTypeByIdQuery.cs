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

namespace RealStateApp.Core.Application.Features.PropertyType.Queries.GetPropertyTypeById
{
    public class GetPropertyTypeByIdQuery : IRequest<PropertyTypeViewModel>
    {
        public int Id { get; set; }
    }

    public class GetPropertyTypeByIdHandler : IRequestHandler<GetPropertyTypeByIdQuery, PropertyTypeViewModel>
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;
        public GetPropertyTypeByIdHandler(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
        }

        public async Task<PropertyTypeViewModel> Handle(GetPropertyTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var propertyType = await GetAllViewModelWithFilters(request.Id);
            if (propertyType == null) throw new Exception("No Existe este Id");

            return propertyType;
        }

        private async Task<PropertyTypeViewModel> GetAllViewModelWithFilters(int Id)
        {
            var propertyType = await _propertyTypeRepository.GetByIdAsync(Id);
            var propertyTypeVm = _mapper.Map<PropertyTypeViewModel>(propertyType);
            return propertyTypeVm;
        }
    }
}

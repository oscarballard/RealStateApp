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

namespace RealStateApp.Core.Application.Features.PropertyType.Commands.UpdatePropertyType
{
    public class UpdatePropertyTypeCommand : IRequest<PropertyTypeViewModel>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class UpdatePropertyTypeHandler : IRequestHandler<UpdatePropertyTypeCommand, PropertyTypeViewModel>
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;
        public UpdatePropertyTypeHandler(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
        }

        public async Task<PropertyTypeViewModel> Handle(UpdatePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            var propertyType = await _propertyTypeRepository.GetByIdAsync(request.Id);

            if (propertyType == null) throw new Exception("No Existe este Id");

            propertyType = _mapper.Map<Domain.Entities.PropertyType>(request);

            await _propertyTypeRepository.UpdateAsync(propertyType, propertyType.Id);

            var response = _mapper.Map<PropertyTypeViewModel>(propertyType);

            return response;
        }
    }
}

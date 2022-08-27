using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.PropertyType.Commands.CreatePropertyType
{
    public class CreatePropertyTypeCommand : IRequest<int>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class CreatePropertyTypeCommandHandler : IRequestHandler<CreatePropertyTypeCommand, int>
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;
        public CreatePropertyTypeCommandHandler(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreatePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            var propertyType = _mapper.Map<Domain.Entities.PropertyType>(request);

            var response = await _propertyTypeRepository.AddAsync(propertyType);

            return response.Id;
        }
    }
}

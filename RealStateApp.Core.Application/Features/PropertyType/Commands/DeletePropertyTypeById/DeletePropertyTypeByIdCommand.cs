using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.PropertyType.Commands.DeletePropertyTypeById
{
    public class DeletePropertyTypeByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeletePropertyTypeByIdCommandHandler : IRequestHandler<DeletePropertyTypeByIdCommand, int>
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;
        public DeletePropertyTypeByIdCommandHandler(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeletePropertyTypeByIdCommand request, CancellationToken cancellationToken)
        {
            var propertyType = await _propertyTypeRepository.GetByIdAsync(request.Id);

            if (propertyType == null) throw new Exception("No Existe este Id");

            await _propertyTypeRepository.DeleteAsync(propertyType);

            return propertyType.Id;
        }
    }
}

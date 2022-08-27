using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.SalesType.Commands.CreateSalesType
{
    public class CreateSalesTypeCommand : IRequest<int>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class CreateSalesTypeCommandHandler : IRequestHandler<CreateSalesTypeCommand, int>
    {
        private readonly ISalesTypeRepository _salesTypeRepository;
        private readonly IMapper _mapper;
        public CreateSalesTypeCommandHandler(ISalesTypeRepository salesTypeRepository, IMapper mapper)
        {
            _salesTypeRepository = salesTypeRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateSalesTypeCommand request, CancellationToken cancellationToken)
        {
            var salesType = _mapper.Map<Domain.Entities.SalesType>(request);

            var response = await _salesTypeRepository.AddAsync(salesType);

            return response.Id;
        }
    }
}

using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Improvements.Commands.CreateImprovements
{
    public class CreateImprovementsCommand : IRequest<int>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class CreateImprovementsCommandHandler : IRequestHandler<CreateImprovementsCommand, int>
    {
        private readonly IImprovementsRepository _improvementsRepository;
        private readonly IMapper _mapper;
        public CreateImprovementsCommandHandler(IImprovementsRepository improvementsRepository, IMapper mapper)
        {
            _improvementsRepository = improvementsRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateImprovementsCommand request, CancellationToken cancellationToken)
        {
            var salesType = _mapper.Map<Domain.Entities.Improvements>(request);

            var response = await _improvementsRepository.AddAsync(salesType);

            return response.Id;
        }
    }
}

using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.SalesType.Commands.DeleteImprovementsById
{
    public class DeleteImprovementsByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteImprovementsByIdCommandHandler : IRequestHandler<DeleteImprovementsByIdCommand, int>
    {
        private readonly IImprovementsRepository _improvementsRepository;
        private readonly IMapper _mapper;
        public DeleteImprovementsByIdCommandHandler(IImprovementsRepository improvementsRepository, IMapper mapper)
        {
            _improvementsRepository = improvementsRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteImprovementsByIdCommand request, CancellationToken cancellationToken)
        {
            var improvements = await _improvementsRepository.GetByIdAsync(request.Id);

            if (improvements == null) throw new Exception("No Existe este Id");

            await _improvementsRepository.DeleteAsync(improvements);

            return improvements.Id;
        }
    }
}

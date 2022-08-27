using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.ViewModels.Improvements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Improvements.Commands.UpdateImprovements
{
    public class UpdateImprovementsCommand : IRequest<ImprovementsViewModel>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class UpdateImprovementsHandler : IRequestHandler<UpdateImprovementsCommand, ImprovementsViewModel>
    {
        private readonly IImprovementsRepository _improvementsRepository;
        private readonly IMapper _mapper;
        public UpdateImprovementsHandler(IImprovementsRepository improvementsRepository, IMapper mapper)
        {
            _improvementsRepository = improvementsRepository;
            _mapper = mapper;
        }

        public async Task<ImprovementsViewModel> Handle(UpdateImprovementsCommand request, CancellationToken cancellationToken)
        {
            var improvements = await _improvementsRepository.GetByIdAsync(request.Id);

            if (improvements == null) throw new Exception("No Existe este Id");

            improvements = _mapper.Map<Domain.Entities.Improvements>(request);

            await _improvementsRepository.UpdateAsync(improvements, improvements.Id);

            var response = _mapper.Map<ImprovementsViewModel>(improvements);

            return response;
        }
    }
}

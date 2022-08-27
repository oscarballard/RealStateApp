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

namespace RealStateApp.Core.Application.Features.Improvements.Queries.GetImprovementsById
{
    public class GetImprovementsByIdQuery : IRequest<ImprovementsViewModel>
    {
        public int Id { get; set; }
    }

    public class GetImprovementsByIdHandler : IRequestHandler<GetImprovementsByIdQuery, ImprovementsViewModel>
    {
        private readonly IImprovementsRepository _improvementsRepository;
        private readonly IMapper _mapper;
        public GetImprovementsByIdHandler(IImprovementsRepository improvementsRepository, IMapper mapper)
        {
            _improvementsRepository = improvementsRepository;
            _mapper = mapper;
        }

        public async Task<ImprovementsViewModel> Handle(GetImprovementsByIdQuery request, CancellationToken cancellationToken)
        {
            var improvements = await GetAllViewModelWithFilters(request.Id);
            if (improvements == null) throw new Exception("No Existe este Id");

            return improvements;
        }

        private async Task<ImprovementsViewModel> GetAllViewModelWithFilters(int Id)
        {
            var improvements = await _improvementsRepository.GetByIdAsync(Id);
            var improvementsVm = _mapper.Map<ImprovementsViewModel>(improvements);
            return improvementsVm;
        }
    }
}

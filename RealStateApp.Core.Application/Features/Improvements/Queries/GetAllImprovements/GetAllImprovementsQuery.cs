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

namespace RealStateApp.Core.Application.Features.Improvements.Queries.GetAllImprovements
{
    public class GetAllImprovementsQuery : IRequest<ICollection<ImprovementsViewModel>>
    {

    }

    public class GetAllImprovementsQueryHandler : IRequestHandler<GetAllImprovementsQuery, ICollection<ImprovementsViewModel>>
    {
        private readonly IImprovementsRepository _improvementsRepository;
        private readonly IMapper _mapper;
        public GetAllImprovementsQueryHandler(IImprovementsRepository improvementsRepository, IMapper mapper)
        {
            _improvementsRepository = improvementsRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<ImprovementsViewModel>> Handle(GetAllImprovementsQuery request, CancellationToken cancellationToken)
        {
            var improvementsList = await GetAllViewModelWithFilters();
            if (improvementsList == null || improvementsList.Count == 0) throw new Exception("No hay Tipos De Ventas");

            return improvementsList;
        }

        private async Task<ICollection<ImprovementsViewModel>> GetAllViewModelWithFilters()
        {
            var improvementsList = await _improvementsRepository.GetAllAsync();

            var listViewModels = improvementsList.Select(prop => new ImprovementsViewModel
            {
                Id = prop.Id,
                Nombre = prop.Nombre,
                Descripcion = prop.Descripcion
            }).ToList();

            return listViewModels;
        }
    }
}

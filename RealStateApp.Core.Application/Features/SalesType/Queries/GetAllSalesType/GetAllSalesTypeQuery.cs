using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.ViewModels.SalesType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.SalesType.Queries.GetAllSalesType
{
    public class GetAllSalesTypeQuery : IRequest<ICollection<SalesTypeViewModel>>
    {

    }

    public class GetAllSalesTypeQueryHandler : IRequestHandler<GetAllSalesTypeQuery, ICollection<SalesTypeViewModel>>
    {
        private readonly ISalesTypeRepository _salesTypeRepository;
        private readonly IMapper _mapper;
        public GetAllSalesTypeQueryHandler(ISalesTypeRepository salesTypeRepository, IMapper mapper)
        {
            _salesTypeRepository = salesTypeRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<SalesTypeViewModel>> Handle(GetAllSalesTypeQuery request, CancellationToken cancellationToken)
        {
            var salesTypeList = await GetAllViewModelWithFilters();
            if (salesTypeList == null || salesTypeList.Count == 0) throw new Exception("No hay Tipos De Ventas");

            return salesTypeList;
        }

        private async Task<ICollection<SalesTypeViewModel>> GetAllViewModelWithFilters()
        {
            var salesTypeList = await _salesTypeRepository.GetAllAsync();

            var listViewModels = salesTypeList.Select(prop => new SalesTypeViewModel
            {
                Id = prop.Id,
                Nombre = prop.Nombre,
                Descripcion = prop.Descripcion
            }).ToList();

            return listViewModels;
        }
    }
}

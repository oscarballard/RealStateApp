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

namespace RealStateApp.Core.Application.Features.SalesType.Queries.GetSalesTypeById
{
    public class GetSalesTypeByIdQuery : IRequest<SalesTypeViewModel>
    {
        public int Id { get; set; }
    }

    public class GetSalesTypeByIdHandler : IRequestHandler<GetSalesTypeByIdQuery, SalesTypeViewModel>
    {
        private readonly ISalesTypeRepository _salesTypeRepository;
        private readonly IMapper _mapper;
        public GetSalesTypeByIdHandler(ISalesTypeRepository salesTypeRepository, IMapper mapper)
        {
            _salesTypeRepository = salesTypeRepository;
            _mapper = mapper;
        }

        public async Task<SalesTypeViewModel> Handle(GetSalesTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var salesType = await GetAllViewModelWithFilters(request.Id);
            if (salesType == null) throw new Exception("No Existe este Id");

            return salesType;
        }

        private async Task<SalesTypeViewModel> GetAllViewModelWithFilters(int Id)
        {
            var salesType = await _salesTypeRepository.GetByIdAsync(Id);
            var salesTypeVm = _mapper.Map<SalesTypeViewModel>(salesType);
            return salesTypeVm;
        }
    }
}

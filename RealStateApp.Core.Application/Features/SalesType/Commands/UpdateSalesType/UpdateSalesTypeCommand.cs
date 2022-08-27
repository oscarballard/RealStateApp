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

namespace RealStateApp.Core.Application.Features.SalesType.Commands.UpdatePropertyType
{
    public class UpdateSalesTypeCommand : IRequest<SalesTypeViewModel>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class UpdateSalesTypeHandler : IRequestHandler<UpdateSalesTypeCommand, SalesTypeViewModel>
    {
        private readonly ISalesTypeRepository _salesTypeRepository;
        private readonly IMapper _mapper;
        public UpdateSalesTypeHandler(ISalesTypeRepository salesTypeRepository, IMapper mapper)
        {
            _salesTypeRepository = salesTypeRepository;
            _mapper = mapper;
        }

        public async Task<SalesTypeViewModel> Handle(UpdateSalesTypeCommand request, CancellationToken cancellationToken)
        {
            var salesType = await _salesTypeRepository.GetByIdAsync(request.Id);

            if (salesType == null) throw new Exception("No Existe este Id");

            salesType = _mapper.Map<Domain.Entities.SalesType>(request);

            await _salesTypeRepository.UpdateAsync(salesType, salesType.Id);

            var response = _mapper.Map<SalesTypeViewModel>(salesType);

            return response;
        }
    }
}

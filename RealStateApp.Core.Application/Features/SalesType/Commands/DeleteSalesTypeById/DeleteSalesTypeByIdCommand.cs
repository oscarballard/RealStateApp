using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.SalesType.Commands.DeleteSalesTypeById
{
    public class DeleteSalesTypeByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteSalesTypeByIdCommandHandler : IRequestHandler<DeleteSalesTypeByIdCommand, int>
    {
        private readonly ISalesTypeRepository _salesTypeRepository;
        private readonly IMapper _mapper;
        public DeleteSalesTypeByIdCommandHandler(ISalesTypeRepository salesTypeRepository, IMapper mapper)
        {
            _salesTypeRepository = salesTypeRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteSalesTypeByIdCommand request, CancellationToken cancellationToken)
        {
            var salesType = await _salesTypeRepository.GetByIdAsync(request.Id);

            if (salesType == null) throw new Exception("No Existe este Id");

            await _salesTypeRepository.DeleteAsync(salesType);

            return salesType.Id;
        }
    }
}

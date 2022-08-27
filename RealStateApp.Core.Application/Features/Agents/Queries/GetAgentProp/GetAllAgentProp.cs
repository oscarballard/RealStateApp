using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Features.Properties.Queries.GetAllProperties;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockApp.Core.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetAllAgentPropQuery : IRequest<GetAllAgentPropResponse>
    {
        public string Id { get; set; }
    }
    public class GetAllAgentPropQueryHadler : IRequestHandler<GetAllAgentPropQuery, GetAllAgentPropResponse>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IPropertyService _propertyService;


        public GetAllAgentPropQueryHadler(IAccountService accountService, IMapper mapper, IPropertyService propertyService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _propertyService = propertyService;
        }

        public async Task<GetAllAgentPropResponse> Handle(GetAllAgentPropQuery query, CancellationToken cancellationToken)
        {
            var User = await GetByIdViewModel(query.Id);
            if (User == null) throw new Exception($"Agente no encontrado");
            return User;
        }

        private async Task<GetAllAgentPropResponse> GetByIdViewModel(string id)
        {
            var vm = new GetAllAgentPropResponse
            {
                Id = id
            };
            FilterPropertyViewModel filter = new();
            filter.IdAgent = vm.Id;
            vm.Properties = await _propertyService.GetAllViewModelWithFilters(filter);
            return vm;
        }
    }

}

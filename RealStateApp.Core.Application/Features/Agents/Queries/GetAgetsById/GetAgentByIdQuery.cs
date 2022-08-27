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
    public class GetAgentByIdQuery : IRequest<GetAllAgentsResponse>
    {
        public string Id { get; set; }
    }
    public class GetProductByIdQueryHadler : IRequestHandler<GetAgentByIdQuery, GetAllAgentsResponse>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IPropertyService _propertyService;


        public GetProductByIdQueryHadler(IAccountService accountService, IMapper mapper, IPropertyService propertyService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _propertyService = propertyService;
        }

        public async Task<GetAllAgentsResponse> Handle(GetAgentByIdQuery query, CancellationToken cancellationToken)
        {
            var User = await GetByIdViewModel(query.Id);
            if (User == null) throw new Exception($"Agente no encontrado");
            return User;
        }

        private async Task<GetAllAgentsResponse> GetByIdViewModel(string id)
        {
            var userList = await _accountService.GetUserByRol(Roles.Agent.ToString());
            var user = userList.FirstOrDefault(f => f.Id == id);
            var vm = _mapper.Map<GetAllAgentsResponse>(user);
            FilterPropertyViewModel filter = new();
            filter.IdAgent = vm.Id;
            List<PropertyViewModel> propertiesVm = await _propertyService.GetAllViewModelWithFilters(filter);
            vm.PropertiesCount = propertiesVm.Count();
            return vm;
        }
    }

}

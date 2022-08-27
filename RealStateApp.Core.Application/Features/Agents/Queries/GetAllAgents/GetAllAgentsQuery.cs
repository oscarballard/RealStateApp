using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Features.Products.Queries.GetAllProducts;
using RealStateApp.Core.Application.Features.Properties.Queries.GetAllProperties;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetAllAgentsQuery : IRequest<IEnumerable<GetAllAgentsResponse>>
    {
        public bool? IsActive {get; set;}
    }
    public class GetAllAgentsQueryHandler : IRequestHandler<GetAllAgentsQuery, IEnumerable<GetAllAgentsResponse>>
    {
        private readonly IAccountService _accountService;
        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;
        public GetAllAgentsQueryHandler(IAccountService accountService, IMapper mapper, IPropertyService propertyService)
        {
            _accountService = accountService;
            _propertyService = propertyService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllAgentsResponse>> Handle(GetAllAgentsQuery request, CancellationToken cancellationToken)
        {
            var filter =_mapper.Map<GetAllAgentsParameter>(request); 
            var UserViewModel = await  GetAllViewModelWithFilters(filter);
            if (UserViewModel == null || UserViewModel.Count == 0) throw new Exception("Agente no encontrado");
            return UserViewModel;
        }

        private async Task<List<GetAllAgentsResponse>> GetAllViewModelWithFilters(GetAllAgentsParameter filters)
        {
            var AgentList = await _accountService.GetUserByRol(Roles.Agent.ToString());

            var listViewModels = AgentList.Select(agent => new GetAllAgentsResponse
            {
                Id = agent.Id,
                FirstName = agent.FirstName,
                LastName = agent.LastName,
                Email = agent.Email,
                Phone = agent.Phone
            }).ToList();

            List<GetAllAgentsResponse> AgentVm = new();
            FilterPropertyViewModel filter = new();
            List<PropertyViewModel> propertiesVm = new();
            foreach (GetAllAgentsResponse fl in listViewModels)
            {
                filter.IdAgent = fl.Id;
                propertiesVm = await _propertyService.GetAllViewModelWithFilters(filter);
                fl.PropertiesCount = propertiesVm.Count();
                AgentVm.Add(fl);
            }
            return AgentVm;
        }
    }
}

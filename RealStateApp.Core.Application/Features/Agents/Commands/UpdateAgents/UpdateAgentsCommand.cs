using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.User;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockApp.Core.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateAgentsCommand : IRequest<AgentsUpdateResponse>
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
    }
    public class UpdateAgentsCommandHandler : IRequestHandler<UpdateAgentsCommand, AgentsUpdateResponse>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UpdateAgentsCommandHandler(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }
        public async Task<AgentsUpdateResponse> Handle(UpdateAgentsCommand command, CancellationToken cancellationToken)
        {
            var agent = await _accountService.GetUserByIdAsync(command.Id);

            if (agent == null)
            {
                throw new Exception($"Agente no encontrado");
            }
            else
            {
                RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(command);
                registerRequest.IsActive = command.IsActive;
                await _accountService.UpdateUser(registerRequest, agent.Id);

                var agentResponse = _mapper.Map<AgentsUpdateResponse>(registerRequest);
                return agentResponse;
            }
        }
    }
}

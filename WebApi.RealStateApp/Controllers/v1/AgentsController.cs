using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Features.Categories.Queries.GetAllCategories;
using RealStateApp.Core.Application.Features.Properties.Queries.GetAllProperties;
using RealStateApp.Core.Application.Features.Properties.Queries.GetPropertyByCode;
using RealStateApp.Core.Application.Features.Properties.Queries.GetPropertyById;
using RealStateApp.Core.Application.ViewModels.Properties;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.WebApi.Controllers;
using StockApp.Core.Application.Features.Categories.Commands.UpdateCategory;
using StockApp.Core.Application.Features.Categories.Queries.GetCategoryById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RealStateApp.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin,Dev")]
    public class AgentsController : BaseApiController
    {
        [HttpGet]
        [Route("List")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllAgentsResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            try
            {
                var response = await mediator.Send(new GetAllAgentsQuery() { });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllAgentsResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                return Ok(await mediator.Send(new GetAgentByIdQuery() { Id = id}));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAgentProperty/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllAgentPropResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAgentProperty(string id)
        {
            try
            {
                return Ok(await mediator.Send(new GetAllAgentPropQuery() { Id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("ChangeStatus/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveClientAgentViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(string id, UpdateAgentsCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (id != command.Id)
                {
                    return BadRequest();
                }
                return Ok(await mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

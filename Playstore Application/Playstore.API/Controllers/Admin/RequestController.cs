using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playstore.Contracts.DTO.AppInfo;
using Playstore.Contracts.DTO.AppPublishRequest;
using Playstore.Core.Exceptions;
using Playstore.Providers.Handlers.Commands;
using Playstore.Providers.Handlers.Queries.Admin;

namespace Playstore.Controllers.Admin
{
    [ApiController]
    [Route("[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly IMediator mediator;
        public RequestController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("AppRequests")]
        [ProducesResponseType(typeof(IEnumerable<RequestAppInfoDto>) , (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> AppRequests()
        {
            try
            {
                var response = await mediator.Send(new GetAllAppRequestsQuery());
                return Ok(response);
            }
            catch (ApiResponseException error)
            {
                return StatusCode((int) HttpStatusCode.NotFound , error.Message);
            }
        }

        [HttpGet("GetRequestedAppDetails/{appId}")]
        [ProducesResponseType(typeof(RequestAppInfoDto) , (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> GetRequestedAppDetails(Guid appId)
        {
            try
            {
                var appDetails = await mediator.Send(new GetRequestedAppDetailsQuery(appId));

                return Ok(appDetails);
            }
            catch (ApiResponseException error)
            {
                return StatusCode((int)HttpStatusCode.NotFound, error.Message);
            }
        }

        [HttpPost("PublishApp")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> PublishApp(AppPublishDto publishDto)
        {
            try
            {
                var response = await mediator.Send(new AppApprovalCommand(publishDto));
    
                if (response == HttpStatusCode.Created)
                {
                    return StatusCode((int) response , new {message = "Request Executed Successfully"});
                }

                return NoContent();
            }
            catch (ApiResponseException error)
            {
                return NotFound(error.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playstore.ActionFilters;
using Playstore.Contracts.DTO.AppInfo;
using Playstore.Contracts.DTO.AppPublishRequest;
using Playstore.Core.Exceptions;
using Playstore.Providers.Handlers.Commands;
using Playstore.Providers.Handlers.Queries.Admin;

namespace Playstore.Controllers.Admin
{
    [ServiceFilter(typeof(ControllerFilter))]
    [ServiceFilter(typeof(ExceptionHandlerFilter))]
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class RequestController : ControllerBase
    {
        private readonly IMediator mediator;
        public RequestController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("AppRequests")]
        [ProducesResponseType(typeof(IEnumerable<RequestAppInfoDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> AppRequests()
        {
            var response = await mediator.Send(new GetAllAppRequestsQuery());
            return Ok(response);
        }

        [HttpGet("GetRequestedAppDetails/{appId}")]
        [ProducesResponseType(typeof(RequestAppInfoDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> GetRequestedAppDetails(Guid appId)
        {
            var appDetails = await mediator.Send(new GetRequestedAppDetailsQuery(appId));

            return Ok(appDetails);
        }

        [HttpPost("PublishApp")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> PublishApp(AppPublishDto publishDto)
        {
            var response = await mediator.Send(new AppApprovalCommand(publishDto));

            if (response == HttpStatusCode.Created)
            {
                return StatusCode((int)response, new { message = "Request Executed Successfully" });
            }

            return NoContent();
        }
    }
}
using System;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Playstore.Contracts.Data.RoleConfig;
using Playstore.Contracts.DTO.AppPublishRequest;
using Playstore.Providers.Handlers.Commands;
using Playstore.Providers.Handlers.Queries.Admin;

namespace Playstore.Controllers.Admin
{
    [ApiController]
    [Route("[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly IMediator mediator;

        private readonly RoleConfig roleConfig;
        public RequestController(IMediator mediator , IOptions<RoleConfig> options)
        {
            this.mediator = mediator;
            this.roleConfig = options.Value;
        }

        [HttpGet("AppRequests")]
        public async Task<IActionResult> AppRequests()
        {
            var response = await this.mediator.Send(new GetAllAppRequestsQuery());

            if(response.GetType() != typeof(HttpStatusCode))
            {
                return Ok(response);
            }
            
            return StatusCode((int) response);
        }

        [HttpGet("GetRequestedAppDetails/{appId}")]
        public async Task<IActionResult> GetRequestedAppDetails(Guid appId)
        {
            var appDetails = await this.mediator.Send(new GetRequestedAppDetailsQuery(appId));
            
            if(appDetails.GetType() != typeof(HttpStatusCode))
            {
                return Ok(appDetails);
            }

            return StatusCode((int) appDetails);
        }

        [HttpPost("PublishApp")]
        public async Task<IActionResult> PublishApp(AppPublishDto publishDto)
        {
            var response = await this.mediator.Send(new AppApprovalCommand(publishDto));

            return StatusCode((int) response);
        }     
    }
}
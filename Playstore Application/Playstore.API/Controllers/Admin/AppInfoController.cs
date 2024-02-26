using System;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playstore.Contracts.DTO.AppDownloads;
using Playstore.Providers.Handlers.Commands;
using Playstore.Providers.Handlers.Queries;
using Playstore.Providers.Handlers.Queries.Admin;

namespace Playstore.Controllers.Admin
{
    [ApiController]
    [Route("[controller]")]
    public class AppInfoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AppInfoController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("GetAllApps")]
        public async Task<IActionResult> GetAllApps()
        {
            var response = await this._mediator.Send(new GetAllAppsInfoQuery());
            if(response.GetType() != typeof(HttpStatusCode))
            {
                return Ok(response);
            }
            
            return StatusCode((int)(HttpStatusCode)response);
        }

        [HttpDelete("RemoveApp/{id}")]
        public async Task<IActionResult> RemoveApp(Guid id)
        {
            var response = await this._mediator.Send(new RemoveAppInfoCommand(id));

            return StatusCode((int) response);
        }

        [HttpPost("GetAppLogs")]
        public async Task<IActionResult> GetAppLogs(AppLogsDto appLogsDto)
        {
            var response = await this._mediator.Send(new GetAppLogsQuery(appLogsDto));
            
            if(response.GetType() != typeof(HttpStatusCode))
            {
                return Ok(response);
            }

            return StatusCode((int) response);
        }

        // [HttpPost("AddAppReview")]
        // public async Task<IActionResult> AddAppReview(AppReviewDto appDto)
        // {
        //     var response = await this._mediator.Send(new AddAppReviewCommand(appDto));

        //     return StatusCode((int) response);
        // }
    }
}
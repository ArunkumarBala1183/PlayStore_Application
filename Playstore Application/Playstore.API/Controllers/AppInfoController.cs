using System;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playstore.Providers.Handlers.Commands;
using Playstore.Providers.Handlers.Queries;
using Playstore.Providers.Handlers.Queries.Admin;

namespace Playstore.Controllers
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

        [HttpDelete("RemoveApp")]
        public async Task<IActionResult> RemoveApp(Guid id)
        {
            var response = await this._mediator.Send(new RemoveAppInfoCommand(id));

            return StatusCode((int) response);
        }

        [HttpGet("AllAppDownloads")]
        public async Task<IActionResult> AllAppDownloads()
        {
            var response = await this._mediator.Send(new GetAllAppsDownloadsQuery());
            
            if(response.GetType() != typeof(HttpStatusCode))
            {
                return Ok(response);
            }

            return StatusCode((int) response);
        }
    }
}
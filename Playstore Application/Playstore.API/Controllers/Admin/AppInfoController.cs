using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playstore.Contracts.DTO.AppDownloads;
using Playstore.Contracts.DTO.AppInfo;
using Playstore.Contracts.DTO.TotalDownloads;
using Playstore.Core.Exceptions;
using Playstore.Providers.Handlers.Commands;
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

        [HttpPost("GetAllApp")]
        [ProducesResponseType(typeof(IEnumerable<ListAppInfoDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> GetAllApps(GetAllAppsInfoQuery allApps)
        {
            try
            {
                var response = await this._mediator.Send(allApps);

                return Ok(response);
            }
            catch (ApiResponseException error)
            {
                return NotFound(error.Message);
            }
        }

        [HttpDelete("RemoveApp/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> RemoveApp(Guid id)
        {
            try
            {
                var response = await this._mediator.Send(new RemoveAppInfoCommand(id));

                return StatusCode((int)response);
            }
            catch (ApiResponseException error)
            {
                return NotFound(error.Message);
            }
        }

        [HttpPost("GetAppLogs")]
        [ProducesResponseType(typeof(IEnumerable<AppDownloadsDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> GetAppLogs(AppLogsDto appLogsDto)
        {
            try
            {
                var response = await this._mediator.Send(new GetAppLogsQuery(appLogsDto));

                return Ok(response);
            }
            catch (ApiResponseException error)
            {
                return NotFound(new {message = error.Message});                
            }
        }

        [HttpGet("GetTotalDownloads")]
        [ProducesResponseType(typeof(DownloadDetailsDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> GetTotalDownloads()
        {
            try
            {
                var response = await this._mediator.Send(new GetTotalDownloadsQuery());

                return Ok(response);
            }
            catch (ApiResponseException error)
            {
                return NotFound(new { message = error.Message });
            }

        }
    }


}
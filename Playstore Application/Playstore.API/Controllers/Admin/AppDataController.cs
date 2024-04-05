using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playstore.ActionFilters;
using Playstore.Core.Exceptions;
using Playstore.Providers.Handlers.Commands;
using Playstore.Providers.Handlers.Queries.Admin;

namespace Playstore.Controllers.Admin
{
    [ServiceFilter(typeof(ControllerFilter))]
    [ServiceFilter(typeof(ExceptionHandlerFilter))]
    [ApiController]
    [Route("[controller]")]
    public class AppDataController : ControllerBase
    {
        private readonly IMediator mediator;
        public AppDataController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAppData/{appId}")]
        [ProducesResponseType(typeof(FileStream), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> GetAppData(Guid appId)
        {

            var appDetails = await this.mediator.Send(new GetRequestedAppDataQuery(appId));

            return File(appDetails.AppFile, appDetails.ContentType, $"{DateOnly.FromDateTime(DateTime.Now)}");


        }

        [Authorize(Roles = "Admin,")]
        [HttpPost("UploadApp")]
        [ProducesResponseType(typeof(FileStream), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        [AllowAnonymous]
        public async Task<IActionResult> UploadApp([FromForm] AppUploadCommand appData)
        {
            var response = await this.mediator.Send(appData);

            return StatusCode((int)response);

        }
    }
}
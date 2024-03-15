using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playstore.Core.Exceptions;
using Playstore.Providers.Handlers.Commands;
using Playstore.Providers.Handlers.Queries.Admin;
using Serilog;

namespace Playstore.Controllers.Admin
{
    [ApiController]
    [Route("[controller]")]
    public class AppDataController : ControllerBase
    {
        private readonly IMediator mediator;
        public AppDataController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("GetAppData/{appId}")]
        [ProducesResponseType(typeof(FileStream) , (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> GetAppData(Guid appId)
        {
            try
            {
                var appDetails = await this.mediator.Send(new GetRequestedAppDataQuery(appId));
    
                return File(appDetails.AppFile , appDetails.ContentType , $"{DateOnly.FromDateTime(DateTime.Now)}");
            }
            catch (ApiResponseException error)
            {
                Log.Error(error , error.Message);
                return NotFound(error.Message);
            }
        }

        [HttpPost("UploadApp")]
        [ProducesResponseType(typeof(FileStream) , (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> UploadApp([FromForm] AppUploadCommand appData)
        {
            try
            {
                var response = await this.mediator.Send(appData);
    
                return StatusCode((int) response);
            }
            catch (ApiResponseException error)
            {
                return NotFound(error.Message);
            }
        }
    }
}
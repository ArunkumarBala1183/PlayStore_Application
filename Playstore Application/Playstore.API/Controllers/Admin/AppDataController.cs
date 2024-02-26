using System;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playstore.Contracts.DTO.AppData;
using Playstore.Providers.Handlers.Commands;
using Playstore.Providers.Handlers.Queries.Admin;

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
        public async Task<IActionResult> GetAppData(Guid appId)
        {
            var response = await this.mediator.Send(new GetRequestedAppDataQuery(appId));

            if(response.GetType() != typeof(HttpStatusCode))
            {
                RequestedAppDataDto appDetails = (RequestedAppDataDto) response;

                return File(appDetails.AppFile , appDetails.ContentType , "SampleFile");
            }

            return StatusCode((int) response);
        }

        [HttpPost("UploadApp")]
        public async Task<IActionResult> UploadApp([FromForm] AppUploadCommand appData)
        {
            var response = await this.mediator.Send(appData);

            if(response.GetType() != typeof(HttpStatusCode))
            {
                RequestedAppDataDto appDetails = (RequestedAppDataDto) response;

                return File(appDetails.AppFile , appDetails.ContentType , "SampleFile");
            }

            return StatusCode((int) response);
        }
    }
}
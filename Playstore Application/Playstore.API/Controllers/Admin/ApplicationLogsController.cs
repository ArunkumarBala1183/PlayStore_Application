using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playstore.ActionFilters;
using Playstore.Contracts.Data.Entities;
using Playstore.Core.Exceptions;
using Playstore.Providers.Handlers.Queries.Admin;

namespace Playstore.Controllers.Admin
{
    [ServiceFilter(typeof(ControllerFilter))]
    [ApiController]
    [Route("[controller]")]
    public class ApplicationLogsController : ControllerBase
    {
        private readonly IMediator mediator;
        public ApplicationLogsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("GetApplicationLogs")]
        [ProducesResponseType(typeof(IEnumerable<AppLog>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> GetApplicationLogs()
        {
            try
            {
                var response = await mediator.Send(new GetAllApplicationLogsQuery());
                return Ok(response);
            }
            catch (ApiResponseException error)
            {
                return NotFound(new {message = error.Message});
            }
        }


    }
}
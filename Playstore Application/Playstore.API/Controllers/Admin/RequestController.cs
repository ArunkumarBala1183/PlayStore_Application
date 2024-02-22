using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playstore.Providers.Handlers.Queries;
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
        public async Task<IActionResult> AppRequests()
        {
            var response = await this.mediator.Send(new GetAllAppRequestsQuery());

            if(response.GetType() != typeof(HttpStatusCode))
            {
                return Ok(response);
            }
            
            return StatusCode((int) response);
        }
        
    }
}
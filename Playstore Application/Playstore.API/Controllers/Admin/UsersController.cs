using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playstore.Contracts.DTO.UserInfo;
using Playstore.Core.Exceptions;
using Playstore.Providers.Handlers.Queries.Admin;

namespace Playstore.Controllers.Admin
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;
        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("GetAllUsers")]
        [ProducesResponseType(typeof(UserInfoDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var response = await mediator.Send(new GetAllUsersInfoQuery());

                return Ok(response);
            }
            catch (ApiResponseException error)
            {
                return NotFound(error.Message);
            }
        }

        [HttpPost("SearchUserDetails")]
        [ProducesResponseType(typeof(UserInfoDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> SearchUserDetails([FromBody] GetUserDetailsQuery searchDetails)
        {
            try
            {
                var response = await mediator.Send(searchDetails);

                return Ok(response);
            }
            catch (ApiResponseException error)
            {
                return NotFound(new {message = error.Message});
            }
        }
    }
}
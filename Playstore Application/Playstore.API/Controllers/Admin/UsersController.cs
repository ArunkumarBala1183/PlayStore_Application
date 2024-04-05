using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playstore.ActionFilters;
using Playstore.Contracts.DTO.UserInfo;
using Playstore.Core.Exceptions;
using Playstore.Providers.Handlers.Queries.Admin;

namespace Playstore.Controllers.Admin
{
    [ServiceFilter(typeof(ControllerFilter))]
    [ServiceFilter(typeof(ExceptionHandlerFilter))]
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
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

            var response = await mediator.Send(new GetAllUsersInfoQuery());

            return Ok(response);

        }

        [HttpPost("SearchUserDetails")]
        [ProducesResponseType(typeof(UserInfoDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> SearchUserDetails([FromBody] GetUserDetailsQuery searchDetails)
        {
            var response = await mediator.Send(searchDetails);

            return Ok(response);
        }
    }
}
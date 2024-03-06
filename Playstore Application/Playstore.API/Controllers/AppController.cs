using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Playstore.Contracts.DTO;
using Playstore.Core.Exceptions;
using Playstore.Providers.Handlers.Commands;
using Playstore.Providers.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Playstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]       
        [ProducesResponseType(typeof(IEnumerable<AppDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [Authorize]
        [NonAction]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllAppQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }       

        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [NonAction]
        public async Task<IActionResult> Post([FromBody] CreateAppDTO model)
        {
            try
            {
                var command = new CreateAppCommand(model);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(AppDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        [NonAction]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var query = new GetAppByIdQuery(id);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
        }
    }
}
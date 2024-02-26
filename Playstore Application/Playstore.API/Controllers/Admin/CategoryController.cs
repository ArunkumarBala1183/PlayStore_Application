using System;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playstore.Contracts.DTO.Category;
using Playstore.Providers.Handlers.Commands;
using Playstore.Providers.Handlers.Queries.Admin;

namespace Playstore.Controllers.Admin
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto category)
        {
            var response = await this._mediator.Send(new AddCategoryCommand(category));
            
            return StatusCode((int) response);
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await this._mediator.Send(new GetAllCategoryQuery());

            if(response.GetType() != typeof(HttpStatusCode))
            {
                return Ok(response);
            }

            return StatusCode((int)(HttpStatusCode)response);
        }
        //returning response type
        [HttpPost("SearchCategory")]
        public async Task<IActionResult> SearchCategory([FromBody] CategoryDto category)
        {
            var response = await this._mediator.Send(new SearchCategoryQuery(category));

            if(response.GetType() != typeof(HttpStatusCode))
            {
                return Ok(response);
            }

            return StatusCode((int)(HttpStatusCode)response);
        }

        [HttpGet("GetCategory/{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var response = await this._mediator.Send(new GetCategoryQuery(id));

            if(response.GetType() != typeof(HttpStatusCode))
            {
                return Ok(response);
            }

            return StatusCode((int)(HttpStatusCode)response);
        }

        [HttpPatch("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateDto category)
        {
            var response = await this._mediator.Send(new UpdateCategoryCommand(category));

            return StatusCode((int) response);
        }
    }
}
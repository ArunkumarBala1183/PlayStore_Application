using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playstore.ActionFilters;
using Playstore.Contracts.DTO.Category;
using Playstore.Core.Exceptions;
using Playstore.Providers.Handlers.Commands;
using Playstore.Providers.Handlers.Queries.Admin;

namespace Playstore.Controllers.Admin
{
    [ServiceFilter(typeof(ControllerFilter))]
    [ServiceFilter(typeof(ExceptionHandlerFilter))]
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
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

            if (response == HttpStatusCode.AlreadyReported)
            {
                return StatusCode((int)response, new { message = "Category Already Exists" });
            }

            return StatusCode((int)response, new { message = "Category Added" });
        }

        [HttpGet("GetAllCategories")]
        [ProducesResponseType(typeof(IEnumerable<CategoryUpdateDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> GetAllCategories()
        {

            var response = await this._mediator.Send(new GetAllCategoryQuery());

            return Ok(response);
        }

        [HttpPost("SearchCategory")]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> SearchCategory([FromBody] CategoryDto category)
        {
            var response = await this._mediator.Send(new SearchCategoryQuery(category));

            return Ok(response);
        }

        [HttpGet("GetCategory/{id}")]
        [ProducesResponseType(typeof(CategoryUpdateDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var response = await this._mediator.Send(new GetCategoryQuery(id));

            return Ok(response);

        }

        [HttpPatch("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateDto category)
        {
            var response = await this._mediator.Send(new UpdateCategoryCommand(category));

            return StatusCode((int)response, new { message = "Updated Successfully" });
        }
    }
}
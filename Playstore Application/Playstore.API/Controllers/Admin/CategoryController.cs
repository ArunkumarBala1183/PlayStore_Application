using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playstore.ActionFilters;
using Playstore.Contracts.DTO.Category;
using Playstore.Core.Exceptions;
using Playstore.Providers.Handlers.Commands;
using Playstore.Providers.Handlers.Queries.Admin;

namespace Playstore.Controllers.Admin
{
    [ServiceFilter(typeof(ControllerFilter))]
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
            try
            {
                var response = await this._mediator.Send(new AddCategoryCommand(category));

                if (response == HttpStatusCode.AlreadyReported)
                {
                    return StatusCode((int)response, new {message = "Category Already Exists"});
                }

                return StatusCode((int)response, new {message = "Category Added"});
            }
            catch (ApiResponseException error)
            {
                return NotFound(new {message = error.Message});
            }
        }

        [HttpGet("GetAllCategories")]
        [ProducesResponseType(typeof(IEnumerable<CategoryUpdateDto>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var response = await this._mediator.Send(new GetAllCategoryQuery());

                return Ok(response);
            }
            catch (ApiResponseException error)
            {
                return NotFound(new {message = error.Message});
            }
        }

        [HttpPost("SearchCategory")]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> SearchCategory([FromBody] CategoryDto category)
        {
            try
            {
                var response = await this._mediator.Send(new SearchCategoryQuery(category));

                return Ok(response);
            }
            catch (ApiResponseException error)
            {
                return NotFound(new {message = error.Message});
            }
        }

        [HttpGet("GetCategory/{id}")]
        [ProducesResponseType(typeof(CategoryUpdateDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ApiResponseException))]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            try
            {
                var response = await this._mediator.Send(new GetCategoryQuery(id));

                return Ok(response);
            }
            catch (ApiResponseException error)
            {
                return NotFound(new {message = error.Message});
            }

        }

        [HttpPatch("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateDto category)
        {
            try
            {
                var response = await this._mediator.Send(new UpdateCategoryCommand(category));

                return StatusCode((int)response, new { message = "Updated Successfully" });
            }
            catch (ApiResponseException error)
            {
                return NotFound(new {message = error.Message});
            }
        }
    }
}
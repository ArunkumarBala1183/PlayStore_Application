using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO;
using Playstore.Contracts.DTO.AppDownloads;
using Playstore.Contracts.DTO.AppReview;
using Playstore.Contracts.DTO.Category;
using Playstore.Core.Exceptions;
using Playstore.Migrations;
using Playstore.Providers.Handlers.Commands.UserData;
using Playstore.Providers.Handlers.Queries.UserData;
namespace Playstore.Controllers.UserData
{
    [ApiController]
    [Route("[controller]")]
    public class AppInfoController : ControllerBase
    {


        private readonly IMediator _mediator;
        public Guid Assignvalue;
        public AppInfoController(IMediator mediator)
        {
            _mediator = mediator;


        }

        // To Get All the apps  
        [HttpGet("GetAllApps")]
        [ProducesResponseType(typeof(IEnumerable<AppInfoDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Get()
        {
            try
            {
                var query = new GetAllAppInfoQuery();
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
        }

        // To get the reviews for the app
        [HttpGet("ReviewDetails")]
        [ProducesResponseType(typeof(IEnumerable<AppInfoDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> GetDetails(Guid appId)
        {
            try
            {
                var query = new GetAllAppReviewDetails(appId);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }

        }

        // To upload the application
        [HttpPost("AppDetails")]
        [ProducesResponseType(typeof(CreateAppInfoDTO), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Post([FromForm] CreateAppInfoDTO model)
        {
        Console.WriteLine("came.....................");
         
            try
            {
                var command = new CreateAppInfoCommand(model);

                var response = await _mediator.Send(command);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
        }
        //Getting Individual App Details Using "GetAppById"
        [HttpGet("GetAppById")]
        [ProducesResponseType(typeof(AppInfoDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> GetById(Guid appId)
        {
            try
            {
                var query = new GetAppByIdValueQuery(appId);
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

        //Getting Developer My App Details Using "DeveloperMyAppDetails"
        [HttpGet("DeveloperMyAppDetails")]
        [ProducesResponseType(typeof(AppInfoDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<object> GetDeveloperDetails(Guid userId)
        {
            try
            {
                var query = new GetDeveloperMyAppDetails(userId);
                var response = await _mediator.Send(query);

                return response;
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

        //Getting MyDownload Details Using "DownloadsDetails"
        [HttpGet("DownloadsDetails")]
        [ProducesResponseType(typeof(AppDownloadsDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> GetDownloadDetails(Guid userId)
        {
            try
            {
                var query = new GetAppInfoDownloadFile(userId);
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



        //To be use Download the File 
        [HttpPost]
        [Route("DownloadFile")]
        [ProducesResponseType(typeof(AppDownloadsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DownloadFile(Guid appId , Guid userId)
        {
            try
            {

                var query = new GetAppDataQuery(appId , userId);
                var response = await _mediator.Send(query);
                return Ok(response);
            }

            catch (InvalidRequestBodyException)
            {
                return BadRequest(new BaseResponseDTO 
                {   
                    IsSuccess = false,
                    Errors = new String[] {"Already Downloaded"}
                });
            }
            
            catch (Exception ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
                

            }

        }
        //To be Add Review for Apps
        [HttpPost("AddReview")]
        [ProducesResponseType(typeof(AppreviewDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddReview(AppreviewDTO appreviewDTO)
        {
            try
            {
                Console.WriteLine(appreviewDTO.Commands);
                var command = new CreateAppReviewCommand(appreviewDTO);

                return StatusCode((int)HttpStatusCode.Created, await _mediator.Send(command));
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
        //Get the Category name
        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategory()
        {
            try
            {
                var category = new GetAllCategoryQuery();
                var response = await _mediator.Send(category);
                return Ok(response);
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


    }
}
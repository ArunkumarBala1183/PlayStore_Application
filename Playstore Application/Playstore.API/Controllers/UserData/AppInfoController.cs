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

        public AppInfoController(IMediator mediator)
        {
            _mediator = mediator;


        }
        //Getting  All AppDetails Using "getAllapps"
        [HttpGet("getAllapps")]
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
        //Getting All AppReviewDetails Using "ReviewDetails"
        [HttpGet("ReviewDetails/{appId}")]
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
        //Upload Individual App Details Using "AppDetails"
        [HttpPost("AppDetails")]
        [ProducesResponseType(typeof(CreateAppInfoDTO), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Post([FromForm] CreateAppInfoDTO model)

        {
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
        //Getting Individual App Details Using "GetApps"
        [HttpGet("GetApps/{appId}")]
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
            catch (Exception ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }
        }
        //Getting Developer MyAppDetails Using "DeveloperMyAppDetails"
        [HttpGet("DeveloperMyAppDetails/{userId}")]
        [ProducesResponseType(typeof(AppInfoDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> GetDeveloperDetails(Guid userId)
        {
            try
            {
                var query = new GetDeveloperMyAppDetails(userId);
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
        //Getting MyDownloads Details Using "DownloadsDetails"
        [HttpGet("DownloadsDetails/{userId}")]
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
            catch (Exception ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                });
            }

        }
        //Using "DownloadFile" Route For Downloading Files 
        [HttpPost]
        [Route("DownloadFile")]
        [ProducesResponseType(typeof(AppDownloadsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DownloadFile(AppDownloadsDto appDownloadsDto)
        {
            try
            {

                var query = new GetAppDataQuery(appDownloadsDto);
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
        //Using "AddReview" for Add AppReviews
        [HttpPost("AddReview")]
        [ProducesResponseType(typeof(AppreviewDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddReview(AppreviewDTO appreviewDTO)
        {
            try
            {

                var command = new CreateAppReviewCommand(appreviewDTO);

                return StatusCode((int)HttpStatusCode.Created, await _mediator.Send(command));
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
        //Getting CategoryId  Using "GetCategory"
        [HttpGet("GetCategory/{category}")]
        [ProducesResponseType(typeof(AppreviewDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByCategory(string category)
        {
            try
            {
                var query = new GetAllCategory(category);
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


    }
}
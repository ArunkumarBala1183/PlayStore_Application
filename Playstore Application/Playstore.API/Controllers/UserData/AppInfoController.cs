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
     
        public AppDownloads appInfo=new AppDownloads();
        public AppReviewDetailsDTO reviewDetailsDTO=new AppReviewDetailsDTO();
        public DatabaseContext _Dbcontext;
        private readonly IMediator _mediator;
        public Guid Assignvalue;
        public AppInfoController(IMediator mediator,DatabaseContext Dbcontext)
        {
            _mediator = mediator;
            _Dbcontext=Dbcontext;

        }

        // To Get All the apps  
        [HttpGet("GetAllApps")]
        [ProducesResponseType(typeof(IEnumerable<AppInfoDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Get()
        {
            try{
            var query = new GetAllAppInfoQuery();
            var response=await _mediator.Send(query);
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

        // to get the reviews for the app
        [HttpGet("ReviewDetails")]
        [ProducesResponseType(typeof(IEnumerable<AppInfoDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> GetDetails(Guid id)
        {
             try
            {
                var query = new GetAllAppReviewDetails(id);
                var response = await _mediator.Send(query);
                // reviewDetailsDTO.AppId=id;
                // var sum=_Dbcontext.AppReviews.Where(x=>x.AppId==id).Sum(y=>y.Rating);
                // var Count=_Dbcontext.AppReviews.Count(x=>x.AppId==id);
                // if(Count==0)
                // {
                //     return BadRequest(Count);
                // }
                // reviewDetailsDTO.AppCount=Count;
                // reviewDetailsDTO.AvergeRatings= (double)sum/Count;
                
                // reviewDetailsDTO.Commands =_Dbcontext.AppReviews.Where(x => x.AppId == id).ToDictionary(x => x.UserId, x => x.Comment.Split(',').ToList());
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
      
        // to upload the application
        [HttpPost("AppDetails")]
        [ProducesResponseType(typeof(CreateAppInfoDTO), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Post([FromForm]CreateAppInfoDTO model)
        {
            Console.WriteLine("UserId:" +model.Name);
            Console.WriteLine("CategoryId:" +model.CategoryId);
            Console.WriteLine("PublisherName:" +model.PublisherName);
            Console.WriteLine("AppFile:" +model.AppFile + "");
            Console.WriteLine("Logo:" +model.Logo + "");
            Console.WriteLine("Description:" +model.Description);
            Console.WriteLine("AppScreenshots:" +model.appImages + "");
           try
            {
                    var command = new CreateAppInfoCommand(model);

                    var response = await _mediator.Send(command);

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

        [HttpGet("GetAppById")]
        [ProducesResponseType(typeof(AppInfoDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var query = new GetAppByIdValueQuery(id);
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
        [HttpGet("DeveloperMyAppDetails")]
        [ProducesResponseType(typeof(AppInfoDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<object> GetDeveloperDetails(Guid id)
        {
            try{
                var query = new GetDeveloperMyAppDetails(id);
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
        [HttpGet("DownloadsDetails")]
        [ProducesResponseType(typeof(AppDownloadsDto), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> GetDownloadDetails(Guid Userid)
        {
            try{
                var query = new GetAppInfoDownloadFile(Userid);
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
        

        

        [HttpPost]
        [Route("DownloadFile")]
        [ProducesResponseType(typeof(AppDownloadsDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DownloadFile(AppDownloadsDto appDownloadsDto)
        {
            try
            {   
                Console.WriteLine(appDownloadsDto.AppId);
                Console.WriteLine(appDownloadsDto.UserId);

                var query = new GetAppDataQuery(appDownloadsDto.AppId);
                var response = await _mediator.Send(query);
                Console.WriteLine(response);
               if (response.GetType() != typeof(HttpStatusCode))
               {
                 Console.WriteLine(appDownloadsDto.AppId);
                 var fileEntity = _Dbcontext.AppDownloads.FirstOrDefault(f => f.AppId == appDownloadsDto.AppId && f.UserId == appDownloadsDto.UserId);
 
                 if (fileEntity == null)
                 {
                     var entity = new AppDownloads
                     {
                         AppId = appDownloadsDto.AppId,
                         UserId = appDownloadsDto.UserId,
                         DownloadedDate=DateTime.Today,
                     };
                     _Dbcontext.AppDownloads.Add(entity);
                     _Dbcontext.SaveChanges();
                     return Ok(response);
                 }
                 return Ok(new { status = "File Already Download" });
               }


               return BadRequest("No Data");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        

        [HttpPost("AddReview")]
       [ProducesResponseType(typeof(AppreviewDTO),StatusCodes.Status200OK)]
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
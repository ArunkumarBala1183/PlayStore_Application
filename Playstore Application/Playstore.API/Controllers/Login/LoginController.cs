using System.Net;
using System.Threading.Tasks;
using Playstore.Contracts.DTO;
using Playstore.Core.Exceptions;
using Playstore.Providers.Handlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Playstore.Providers.Handlers.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Playstore.Core.Data.Repositories;
using Playstore.Providers.Handlers.Queries.Admin;

namespace Playstore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly SharedDataService _sharedDataService;


        public LoginController(IMediator mediator, SharedDataService sharedDataService)
        {
            _mediator = mediator;
            _sharedDataService = sharedDataService;
        }

        [HttpPost("GetUserbyId")]
        [Authorize(Roles = "dev")]
        //[Authorize]
        [ProducesResponseType(typeof(List<AllUsersDTO>), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]

        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetAllUsersQuery(id);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost("update-permissions")]

        public async Task<IActionResult> UpdatePermissions([FromBody] UpdatePermissionsCommand model, [FromQuery] bool allow)
        {
            //var command = new UpdatePermissionsCommand(model) { Allow = allow };
            var updatedToken = await _mediator.Send(model);
            return Ok(new { UpdatedToken = updatedToken });
        }

        // [HttpGet("CheckEmailExistence")]
        // [ProducesResponseType(typeof(bool), (int)HttpStatusCode.Created)]
        // [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        // public async Task<IActionResult> CheckEmailExistence(string email)
        // {
        //     Console.WriteLine(email);
        //     var query = new CheckEmailExistenceQuery(email);

        //     var result = await _mediator.Send(query);

        //     return Ok(result); 
        // }

        // [HttpPost("CheckEmailExistence")]
        // [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        // [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        // public async Task<IActionResult> CheckEmailExistence(  ForgotPasswordDTO model)
        // {

        //     Console.WriteLine(model.EmailId);
        //     var query = new CheckEmailExistenceQuery(model.EmailId);

        //     var result = await _mediator.Send(query);

        //     return Ok(result); 
        // }
        [HttpPost("CheckEmailExistence")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> CheckEmailExistence(ForgotPasswordDTO model)
        {
            try
            {
                Console.WriteLine(model?.EmailId);
                if (model == null)
                {
                    // Log or handle the case where model is null
                    return BadRequest("Invalid model");
                }
                Console.WriteLine(model.EmailId);
                var query = new CheckEmailExistenceQuery(model.EmailId);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception in CheckEmailExistence: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpPost("forgot-Password")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(object))]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO model)
        {
            try
            {
                var otp = await _mediator.Send(new ForgotPasswordCommand(model));
                HttpContext.Session.SetString("ResetPasswordEmail", model.EmailId);
                var ResetPasswordEmail = HttpContext.Session.GetString("ResetPasswordEmail");
                _sharedDataService.ResetPasswordEmail = HttpContext.Session.GetString("ResetPasswordEmail");
                Console.WriteLine(".............This is email" + ResetPasswordEmail);


                HttpContext.Session.SetString("ResetPasswordOTP", otp);
                var ResetPasswordOTP = HttpContext.Session.GetString("ResetPasswordOTP");
                _sharedDataService.ResetPasswordOTP = HttpContext.Session.GetString("ResetPasswordOTP");
                Console.WriteLine("...................This is otp" + ResetPasswordOTP);

                return Ok(new { Message = "OTP sent successfully." });
            }
            catch (Exception)
            {
                return NotFound(new { Message = "Email not registered." });
            }
        }


        [HttpPost("validate-otp")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> ValidateOtp([FromBody] ValidateOtpDTO validateOtpDTO)
        {
            try
            {
                var storedOtp = HttpContext.Session.GetString("ResetPasswordOTP");
                var storedEmail = HttpContext.Session.GetString("ResetPasswordEmail");
                var resetPasswordEmail = _sharedDataService.ResetPasswordEmail;
                var resetPasswordOTP = _sharedDataService.ResetPasswordOTP;

                Console.WriteLine("Stored OTP: " + resetPasswordOTP);

                Console.WriteLine("Stored Email: " + resetPasswordEmail);
                Console.WriteLine("Received OTP: " + validateOtpDTO.Otp);

                validateOtpDTO.EmailId = storedEmail;

                var command = new ValidateOtpCommand(validateOtpDTO, resetPasswordEmail, resetPasswordOTP);
                var isOtpValid = await _mediator.Send(command);


                if (isOtpValid)
                {
                    return Ok(new { Message = "OTP validation successful" });
                }
                else
                {
                    return BadRequest(new { Error = "Invalid OTP" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetDTO resetPasswordDTO)
        {
            try
            {
                Console.WriteLine(resetPasswordDTO.NewPassword);
                Console.WriteLine(resetPasswordDTO.ConfirmPassword);
                var resetPasswordEmail = _sharedDataService.ResetPasswordEmail;
                var resetPasswordOTP = _sharedDataService.ResetPasswordOTP;

                Console.WriteLine("Stored OTP: " + resetPasswordOTP);

                Console.WriteLine("Stored Email: " + resetPasswordEmail);
                Console.WriteLine("Received newpassword: " + resetPasswordDTO.NewPassword);

                Console.WriteLine("Received confirmpassword: " + resetPasswordDTO.ConfirmPassword);
                var command = new ResetPasswordCommand(resetPasswordDTO, resetPasswordEmail, resetPasswordOTP);
                var isPasswordReset = await _mediator.Send(command);

                if (isPasswordReset)
                {
                    return Ok(new { Message = "Password reset successful" });
                }
                else
                {
                    Console.WriteLine("Failed");
                    return BadRequest(new { Error = "Password reset failed. Make sure OTP is validated first." });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { Error = ex.Message });
            }
        }



        [HttpPost("register")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> UserRegistration([FromBody] RegisterUsersDTO model)
        {
            try
            {
                if (model == null)
                {
                    // Log or handle the case where model is null
                    return BadRequest("Invalid model");
                }
                Console.WriteLine(model.Name);
                var command = new RegisterUsersCommand(model);
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
            catch (DuplicateEmailException ex)
            {
                return Conflict(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new[] { ex.Message }
                });
            }
        }



        [HttpPost("User-Login")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> UserLogin([FromBody] LoginUsersDTO model)
        {
            Console.WriteLine(model.EmailId);
            Console.WriteLine(model.Password);
            try
            {

                var command = new LoginUsersCommand(model);
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
            catch (EntityNotFoundException ex)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new[] { ex.Message }
                });
            }
            catch (InvalidcredentialsException ex)
            {
                return Unauthorized(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new[] { ex.Message }
                });
            }
        }
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            if (command == null)
            {
                // Handle the case where the command is null
                return BadRequest("Invalid request. Command object is null.");
            }
            Console.WriteLine(command.ExpiredToken);

            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new[] { ex.Message }
                });
            }
        }
     [HttpPatch("changePassword")]
     [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
     [ProducesErrorResponseType(typeof(BaseResponseDTO))]
     public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordQuery command)
     {
        Console.WriteLine("...................."+ command.userId);
        Console.WriteLine("................."+ command.password);
        
            try
            {
                var response = await _mediator.Send(command);
                Console.WriteLine(response);
                return Ok(new {message = response}); 
            }
            catch (Exception exception)
            {
                return NotFound(new BaseResponseDTO
                {
                    
                    IsSuccess = false,
                    Errors = new string[] { exception.Message }
                });
            }
        }
        
        [HttpGet("checkPassword")]
        public async Task<IActionResult> CheckPassword(Guid UserId , string password)
        {
            var value=new GetPasswordQuery(UserId ,password);
            var response= await _mediator.Send(value);
            return Ok(response);
            
        }
       

    }
}
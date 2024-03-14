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

        [HttpPost("CheckEmailExistence")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> CheckEmailExistence(ForgotPasswordDTO model)
        {
            try
            {
                var query = new CheckEmailExistenceQuery(model.EmailId);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception)
            {
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
                _sharedDataService.ResetPasswordEmail = HttpContext.Session.GetString("ResetPasswordEmail");


                HttpContext.Session.SetString("ResetPasswordOTP", otp);
                _sharedDataService.ResetPasswordOTP = HttpContext.Session.GetString("ResetPasswordOTP");

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
                var resetPasswordEmail = _sharedDataService.ResetPasswordEmail;
                var resetPasswordOTP = _sharedDataService.ResetPasswordOTP;

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
                var resetPasswordEmail = _sharedDataService.ResetPasswordEmail;
                var resetPasswordOTP = _sharedDataService.ResetPasswordOTP;

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
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> UserRegistration([FromBody] RegisterUsersDTO model)
        {
            try
            {
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
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordQuery command)
        {
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
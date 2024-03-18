using System.Net;
using System.Threading.Tasks;
using Playstore.Contracts.DTO;
using Playstore.Core.Exceptions;
using Playstore.Providers.Handlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using Playstore.Providers.Handlers.Queries;
using Microsoft.AspNetCore.Http;
using Playstore.Core.Data.Repositories;
using Playstore.Providers.Handlers.Queries.Admin;
using System.IO;
using Playstore.ActionFilters;
using Microsoft.AspNetCore.Authorization;
namespace Playstore.Controllers
{
    // [ServiceFilter(typeof(ControllerFilter))]
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

        // To Check whether the Email-Id already exist or not
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
                return StatusCode(500, new{message = "Internal Server Error"} );
            }
        }

        // To Send OTP to the respected Email-Id 
        [HttpPost("forgot-Password")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(object))]
        public async Task<IActionResult> SendOTP([FromBody] ForgotPasswordDTO model)
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
            catch (InvalidcredentialsException)
            {
                return NotFound(new { Message = "Email not registered." });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


        // To validate the given OTP
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
            catch (Exception exception)
            {
                return BadRequest(new { Error = exception.Message });
            }
        }


        // To reset new password after OTP validation
        [HttpPost("reset-password")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetDTO resetPasswordDTO)
        {
            try
            {
                var resetPasswordEmail = _sharedDataService.ResetPasswordEmail;
                var command = new ResetPasswordCommand(resetPasswordDTO, resetPasswordEmail);
                var isPasswordReset = await _mediator.Send(command);

                if (isPasswordReset)
                {
                    
                    return Ok(new { Message = "Password reset successful" });
                    
                }
                else
                {
                    return BadRequest(new { Error = "Password reset failed. Make sure OTP is validated first." });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { Error = exception.Message });
            }
        }


        // Sign-up as new user
        [HttpPost("register")]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> SignUp([FromBody] RegisterUsersDTO model)
        {
            try
            {
                var command = new RegisterUsersCommand(model);
                var response = await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (InvalidRequestBodyException exception)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = exception.Errors
                });
            }
            catch (DuplicateEmailException exception)
            {
                return Conflict(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new[] { exception.Message }
                });
            }
        }


        // Sign-in to Explore the AppStore
        [HttpPost("User-Login")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> SignIn([FromBody] LoginUsersDTO model)
        {
            try
            {

                var command = new LoginUsersCommand(model);
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (InvalidRequestBodyException exception)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = exception.Errors
                });
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new[] { exception.Message }
                });
            }
            catch (InvalidcredentialsException exception)
            {
                return Unauthorized(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new[] { exception.Message }
                });
            }
        }

        
        // To get new AccessToken when old token Expired
        [Authorize]
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
            catch (UnauthorizedAccessException exception)
            {
                return Unauthorized(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new[] { exception.Message }
                });
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new[] { ex.Message }
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = new[] { ex.Message }
                });
            }
        }


        // Changes the currentPassword whenever the user/admin wish to
        [Authorize]
        [HttpPatch("changePassword")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordQuery command)
        {
            try
            {
                var response = await _mediator.Send(command);
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


        // Checks whether the given password is similar to CurrentPassword
        [Authorize]
        [HttpGet("checkPassword")]
        public async Task<IActionResult> CheckPassword(Guid UserId , string password)
        {
            var value=new GetPasswordQuery(UserId ,password);
            var response= await _mediator.Send(value);
            return Ok(response);
            
        }
    }
}
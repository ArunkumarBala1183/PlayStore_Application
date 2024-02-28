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
using Playstore.Contracts.Data.Entities;

namespace Playstore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
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


        [HttpPost("validate-otp")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
    public async Task<IActionResult> ValidateOtp([FromBody] ValidateOtpDTO validateOtpDTO)
    {
        try
        {
            Console.WriteLine("................." + validateOtpDTO.Otp);
            var command = new ValidateOtpCommand(validateOtpDTO);
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
            var command = new ResetPasswordCommand(resetPasswordDTO);
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
        catch (Exception ex)
        {
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

        [HttpPost("forgot-Password")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(object))]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO model)
        {
            var command = new ForgotPasswordCommand(model);

            try
            {
                var result = await _mediator.Send(command);

                if (result)
                {
                    return Ok(new { Message = "OTP sent successfully." });
                }
                else
                {
                    return NotFound(new { Message = "Email not registered." });
                }
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Internal server error." });
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

    }
}
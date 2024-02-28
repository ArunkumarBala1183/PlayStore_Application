using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Commands
{
    public class ValidateOtpCommandHandler : IRequestHandler<ValidateOtpCommand, bool>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateOtpCommandHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

         public Task<bool> Handle(ValidateOtpCommand request, CancellationToken cancellationToken)
    {
        var storedOtp = _httpContextAccessor.HttpContext.Session.GetString("ResetPasswordOTP");
        var storedEmail = _httpContextAccessor.HttpContext.Session.GetString("ResetPasswordEmail");


        Console.WriteLine($"Received OTP: {request.Model.Otp}, Stored OTP: {storedOtp}");

        var isOtpValid = string.Equals(request.Model.Otp, storedOtp, StringComparison.OrdinalIgnoreCase);

        //_httpContextAccessor.HttpContext.Session.Remove("ResetPasswordOTP");
        if (string.IsNullOrEmpty(storedEmail))
            {
                throw new InvalidOperationException("Email not found in session.");
            }

        Console.WriteLine($"Is OTP Valid: {isOtpValid}");

        return Task.FromResult(isOtpValid);
    }
    }
}
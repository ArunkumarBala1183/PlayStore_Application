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
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Commands
{
    

    public class UpdatePermissionsCommandHandler : IRequestHandler<UpdatePermissionsCommand, string>
    {
        private readonly IRoleRepository _userCredentialsRepository;
        private readonly IConfiguration _configuration;

        public UpdatePermissionsCommandHandler(IRoleRepository userCredentialsRepository, IConfiguration configuration)
        {
            _userCredentialsRepository = userCredentialsRepository;
            _configuration = configuration;
        }

        public async Task<string> Handle(UpdatePermissionsCommand request, CancellationToken cancellationToken)
        {
            UpdatePermissionsCommand model = request;

            var existingToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJFbWFpbElkIjoic3dlZGhhMDAyNUBnbWFpbC5jb20iLCJSb2xlcyI6InVzZXIsRGV2ZWxvcGVyIiwiUGFzc3dvcmQiOiJBUUFBQUFFQUFDY1FBQUFBRUdjUDh1WFpnRFR3NG01c0xqWndRTFBNVWxCZ28vTmV1VU9RYnYvN0JEUnR6SVZXTHVoMjFYUGhvRkg5MVNWZHN3PT0iLCJuYmYiOjE3MDg2OTA0NzgsImV4cCI6MTcwOTI5NTI3OCwiaWF0IjoxNzA4NjkwNDc4fQ.IC1QJTvwHYJ6wlPbne5MkR18VkA5in_bElwEqwtXHAQ";
            var tokenHandler = new JwtSecurityTokenHandler();
            var existingTokenClaims = tokenHandler.ReadToken(existingToken) as JwtSecurityToken;

            var userEmailClaim = existingTokenClaims?.Claims.FirstOrDefault(claim => claim.Type == "EmailId")?.Value;
            var userCredentials = await _userCredentialsRepository.GetByEmailAsync(userEmailClaim);
            if (userCredentials == null)
            {
                throw new EntityNotFoundException("User not found");
            }

            var newRoleCode = model.Allow ? $"{model.RoleCode},Developer" : model.RoleCode;
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Authentication:Jwt:Secret"));

            var newClaims = new List<Claim>
            {
                new Claim("EmailId", userCredentials.EmailId),
                new Claim("Roles", newRoleCode),
                new Claim("Password", userCredentials.Password)
            };

            var newTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(newClaims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var newToken = tokenHandler.CreateToken(newTokenDescriptor);
            return newToken == null ? throw new EntityNotFoundException($"Failed to generate the token") : tokenHandler.WriteToken(newToken);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO;
using Playstore.Core.Exceptions;
using Playstore.Providers.Handlers.Commands;
 
namespace Playstore.Providers.Handlers.Commands
{
   
 
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResponse>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserCredentialsRepository _credentialsRepository;
        private readonly IConfiguration _configuration;
 
        public RefreshTokenCommandHandler(IRoleRepository roleRepository,IRefreshTokenRepository refreshTokenRepository,
            IUserCredentialsRepository credentialsRepository, IConfiguration configuration)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _credentialsRepository = credentialsRepository;
            _configuration = configuration;
            _roleRepository = roleRepository;
        }
 
        public async Task<TokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var expiredToken = request.ExpiredToken;
 
            var (userId, refreshToken) = GetClaimsFromExpiredToken(expiredToken);
 
            var refreshTokenEntity = await _refreshTokenRepository.GetRefreshTokenAsync(userId);
 
            var userCredentials = await _credentialsRepository.GetByIdAsync(userId);
 
            var tokenResponse = await GenerateToken(userCredentials);
 
            return tokenResponse;
        }
 
 
        private async Task<TokenResponse> GenerateToken(UserCredentials userCredentials)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Authentication:Jwt:Secret"));
            var refreshTokenEntity = await _refreshTokenRepository.GetRefreshTokenAsync(userCredentials.UserId);
            var userRoles = await _roleRepository.GetUserRolesAsync(userCredentials.UserId);
            var roleCodes = userRoles.Select(ur => ur.Role.RoleCode).ToList();

            //var userRoles = await _credentialsRepository.GetUserRolesAsync(userCredentials.UserId);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.UserData, userCredentials.UserId.ToString()),
                //new Claim(ClaimTypes.Role, "User"),
                //new Claim(ClaimTypes.Expired, refreshTokenEntity.RefreshKey)
            };
            foreach (var roleCode in roleCodes)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleCode));
            }
            if (refreshTokenEntity != null)
            {
                claims.Add(new Claim(ClaimTypes.Expired, refreshTokenEntity.RefreshKey));
            }
 
            var accessTokenExpires = DateTime.UtcNow.AddMinutes(15);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = accessTokenExpires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
 
            var newRefreshToken = GenerateRefreshToken();
           if (newRefreshToken != null)
            {
                await _refreshTokenRepository.StoreRefreshTokenAsync(userCredentials.UserId, newRefreshToken);
            }
            var token = tokenHandler.CreateToken(tokenDescriptor);
 
            return new TokenResponse
            {
                AccessToken = token == null ? throw new EntityNotFoundException($"Failed to generate the token") : tokenHandler.WriteToken(token),
                RefreshToken = newRefreshToken
            };
        }

        private (Guid userId, string refreshToken) GetClaimsFromExpiredToken(string expiredToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var readToken = tokenHandler.ReadToken(expiredToken) as JwtSecurityToken;

            if (readToken?.Claims != null)
            {
                var userIdClaim = readToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData);
                var refreshTokenClaim = readToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Expired);

                if (userIdClaim != null)
                {
                    var userId = Guid.Parse(userIdClaim.Value);
                    var refreshToken = refreshTokenClaim?.Value;

                    return (userId, refreshToken);
                }
            }
            throw new ArgumentException("Invalid or missing claims in the expired token");
        }

 
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
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
    public class LoginUsersCommandHandler : IRequestHandler<LoginUsersCommand, TokenResponse>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IValidator<LoginUsersDTO> _validator;
        private readonly IUserCredentialsRepository _credentialsRepository;
        private readonly IPasswordHasher<UserCredentials> _passwordHasher;
        private readonly IConfiguration _configuration;
 
        public LoginUsersCommandHandler(IUserCredentialsRepository credentialsRepository,
        IRefreshTokenRepository refreshTokenRepository, IConfiguration configuration,
            IValidator<LoginUsersDTO> validator, IPasswordHasher<UserCredentials> passwordHasher)
        {
            _credentialsRepository = credentialsRepository;
            _passwordHasher = passwordHasher;
            _validator = validator;
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
        }
 
        public async Task<TokenResponse> Handle(LoginUsersCommand request, CancellationToken cancellationToken)
        {
            LoginUsersDTO model = request.Model;
            var validationResult = await _validator.ValidateAsync(model);
 
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }
 
            var userCredentials = await _credentialsRepository.GetByEmailAsync(model.EmailId);
            var refreshTokenEntity = await _refreshTokenRepository.GetRefreshTokenAsync(userCredentials.UserId);
 
            if (userCredentials == null)
            {
                throw new EntityNotFoundException("User not found");
            }
 
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(userCredentials, userCredentials.Password, model.Password);
 
            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                throw new InvalidcredentialsException("Invalid password");
            }
 
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Authentication:Jwt:Secret"));
 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.UserData, userCredentials.UserId.ToString()),
                new Claim(ClaimTypes.Role , model.RoleCode),
                // new Claim(ClaimTypes.Expired, refreshTokenEntity.RefreshKey)
            };
            if (refreshTokenEntity != null)
            {
                claims.Add(new Claim(ClaimTypes.Expired, refreshTokenEntity.RefreshKey));
            }
            var accessTokenExpires = DateTime.UtcNow.AddMinutes(15);
            var refreshTokenExpires = DateTime.UtcNow.AddDays(7);
 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = accessTokenExpires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var refreshToken = refreshTokenEntity!= null ? GenerateRefreshToken() : null;
            if(refreshToken != null)
            {
                await StoreRefreshTokenAsync(userCredentials.UserId, refreshToken);
            }
           
            var token = tokenHandler.CreateToken(tokenDescriptor);
 
            return new TokenResponse
            {
                AccessToken = token == null ? throw new EntityNotFoundException($"Failed to generate the token") : tokenHandler.WriteToken(token),
                RefreshToken = refreshToken
            };
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
 
        private async Task StoreRefreshTokenAsync(Guid userId, string refreshToken)
        {
            await _refreshTokenRepository.StoreRefreshTokenAsync(userId, refreshToken);
        }
    }
}
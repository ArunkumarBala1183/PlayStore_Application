using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Commands
{
    public class LoginUsersCommandHandler : IRequestHandler<LoginUsersCommand, TokenResponse>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IValidator<LoginUsersDTO> _validator;
        private readonly IUserCredentialsRepository _credentialsRepository;
        private readonly IPasswordHasher<UserCredentials> _passwordHasher;
        private readonly IConfiguration _configuration;

        public LoginUsersCommandHandler(IRoleRepository roleRepository, IUserCredentialsRepository credentialsRepository,
        IRefreshTokenRepository refreshTokenRepository, IConfiguration configuration,
            IValidator<LoginUsersDTO> validator, IPasswordHasher<UserCredentials> passwordHasher)
        {
            _credentialsRepository = credentialsRepository;
            _passwordHasher = passwordHasher;
            _validator = validator;
            _configuration = configuration;
            _roleRepository = roleRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<TokenResponse> Handle(LoginUsersCommand request, CancellationToken cancellationToken)
        {
            LoginUsersDTO model = request.Model;
            var validationResult = await _validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(validationMessage => validationMessage.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var userCredentials = await _credentialsRepository.GetByEmailAsync(model.EmailId);
           
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(userCredentials, userCredentials.Password, model.Password);

            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                throw new InvalidcredentialsException("Invalid password");
            }
            var userRoles = await _roleRepository.GetUserRolesAsync(userCredentials.UserId);
            var roleCodes = userRoles.Select(obtainRole => obtainRole.Role.RoleCode).ToList();

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Authentication:Jwt:Secret"));
            var refreshToken = GenerateRefreshToken();
            var claims = new List<Claim>
            {

                new (ClaimTypes.UserData, userCredentials.UserId.ToString()),
                new (ClaimTypes.Expired, refreshToken)

            };
            foreach (var roleCode in roleCodes)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleCode));
            }
            await StoreRefreshTokenAsync(userCredentials.UserId, refreshToken);
            var accessTokenExpires = DateTime.Now.AddMinutes(15);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = accessTokenExpires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256Signature)
            };


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
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private async Task StoreRefreshTokenAsync(Guid userId, string refreshToken)
        {
            await _refreshTokenRepository.StoreRefreshTokenAsync(userId, refreshToken);
        }
    }
}



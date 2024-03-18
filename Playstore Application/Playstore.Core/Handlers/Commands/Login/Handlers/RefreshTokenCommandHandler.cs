using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Commands
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResponse>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserCredentialsRepository _credentialsRepository;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommandHandler(IRoleRepository roleRepository, IRefreshTokenRepository refreshTokenRepository,
            IUserCredentialsRepository credentialsRepository, IConfiguration configuration)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _credentialsRepository = credentialsRepository;
            _configuration = configuration;
            _roleRepository = roleRepository;
        }
        public async Task<TokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var expiredToken = request.ExpiredToken ?? throw new EntityNotFoundException("Expired Token not found");
            var (userId, refreshToken) = GetClaimsFromExpiredToken(expiredToken);
            var refreshTokenEntity = await _refreshTokenRepository.GetRefreshToken(userId);

            if (refreshTokenEntity != null && refreshTokenEntity.RefreshKey == refreshToken)
            {
                var userCredentials = await _credentialsRepository.GetById(userId);
                var tokenResponse = await GenerateToken(userCredentials);
                return tokenResponse;
            }

            throw new SecurityTokenException("Invalid or expired refresh token");
        }

        private (Guid userId, string refreshToken) GetClaimsFromExpiredToken(string expiredToken)
{
    try
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var readToken = tokenHandler.ReadToken(expiredToken) as JwtSecurityToken;

        if (readToken?.Claims != null)
        {
            var userIdClaim = readToken.Claims.FirstOrDefault(claims => claims.Type == ClaimTypes.UserData);
            var refreshTokenClaim = readToken.Claims.FirstOrDefault(claims => claims.Type == ClaimTypes.Expired);

            if (userIdClaim != null)
            {
                var userId = Guid.Parse(userIdClaim.Value);
                var refreshToken = refreshTokenClaim?.Value;

                if (!string.IsNullOrEmpty(refreshToken))
                {
                    return (userId, refreshToken);
                }
            }
        }

        throw new UnauthorizedAccessException("Invalid or missing claims in the expired token");
    }
    catch (ArgumentException )
    {
        throw new ArgumentException("Invalid or malformed JWT token");
    }
}


        private async Task<TokenResponse> GenerateToken(UserCredentials userCredentials)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Authentication:Jwt:Secret"));
            var userRoles = await _roleRepository.GetUserRoles(userCredentials.UserId);
            var roleCodes = userRoles.Select(ur => ur.Role.RoleCode).ToList();
            var newRefreshToken = GenerateRefreshToken();

            var claims = new List<Claim>
            {
                new(ClaimTypes.UserData, userCredentials.UserId.ToString()),
                new(ClaimTypes.Expired, newRefreshToken)
            };
            foreach (var roleCode in roleCodes)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleCode));
            }

            await _refreshTokenRepository.StoreRefreshToken(userCredentials.UserId, newRefreshToken);


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
                RefreshToken = newRefreshToken
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
    }
}
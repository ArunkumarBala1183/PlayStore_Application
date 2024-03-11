using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Playstore.Contracts.Middleware
{
    public class JwtAuthenticationMiddleware : IMiddleware
    {
        private readonly IConfiguration configuration;

        public JwtAuthenticationMiddleware(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var token = context.Request.Headers["Authorization"].ToString().Split(" ").Last();
            Console.WriteLine($"From Middlewaretoken : {token}\n");
            if (!string.IsNullOrEmpty(token))
            {
                GetUserIdByToken(token , context);
            }
            await next(context);    
        }

        private void GetUserIdByToken(string token , HttpContext context)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(configuration.GetValue<string>("Authentication:Jwt:Secret"));

            tokenHandler.ValidateToken(token , new TokenValidationParameters{
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            } , out var validatedToken);

            var securityToken = validatedToken as JwtSecurityToken;

            var userId = securityToken.Claims.FirstOrDefault(id => id.Type == ClaimTypes.UserData).Value;

            context.Items["userId"] = userId;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Playstore.Contracts.Middleware
{
    public class JwtAuthenticationMiddleware : IMiddleware
    {
        private readonly IConfiguration configuration;
        private ILogger logger;

        public JwtAuthenticationMiddleware(IConfiguration configuration)
        {
            this.configuration = configuration;
            logger = Log.ForContext("Location" , typeof(JwtAuthenticationMiddleware).Name);
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                var headerValue = context.Request.Headers["Authorization"].ToString().Split(" ");

                var tokenString = headerValue[headerValue.Length - 1];

                if (!string.IsNullOrEmpty(tokenString) && this.ValidateToken(tokenString))
                {
                    JwtSecurityTokenHandler tokenHandler = new();

                    var token = tokenHandler.ReadToken(tokenString) as JwtSecurityToken;

                    if (token != null)
                    {
                        var userId = token.Claims.FirstOrDefault(type => type.Type == ClaimTypes.UserData).Value;
                        context.Items["userId"] = userId;
                        logger = logger.ForContext("userId" , userId);
                        await next(context);
                    }
                    else
                    {
                        logger.Warning("Not a Valid Token");
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsJsonAsync(new { message = "Unauthorized" });
                    }

                }
                else
                {
                    var requestedPath = context.Request.Path.ToString().ToLower(culture: CultureInfo.InvariantCulture);

                    var whiteListUrls = configuration.GetSection("Authentication:WhiteListUrls").Get<List<string>>();

                    if (whiteListUrls != null && whiteListUrls.Count > 0)
                    {
                        if (whiteListUrls.Any(url => requestedPath.Contains(url, StringComparison.OrdinalIgnoreCase)))
                        {
                            await next(context);
                        }
                        else
                        {
                            logger.Warning("Unauthorized Access");
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            await context.Response.WriteAsJsonAsync(new { message = "Unauthorized" });
                        }
                    }
                    else
                    {
                        logger.Error("Internal Server Error");
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsJsonAsync(new { message = "Internal Server Error" });
                    }
                }
            }
            catch (Exception error)
            {
                logger.Error(error, $"Error Message : {error.Message}");
            }
        }

        private bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(configuration.GetValue<string>("Authentication:Jwt:Secret"));

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            if (validatedToken != null)
            {
                return true;
            }
            return false;
        }
    }
}
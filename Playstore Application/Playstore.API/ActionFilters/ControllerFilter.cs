using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Playstore.ActionFilters
{
    public class ControllerFilter : IActionFilter
    {
        private string ControllerActionName;
        private string path = "Login/User-Login";
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.HttpContext.Request.Path.ToString().ToLower().Contains(path.ToLower()))
            {
                Log
                .ForContext("Location", ControllerActionName)
                .Information($"{context.ActionDescriptor.DisplayName} is Executed");
            }
            else
            {
                Log.ForContext("userId", context.HttpContext.Items["userId"])
                .ForContext("Location", ControllerActionName)
                .Information($"{context.ActionDescriptor.DisplayName} is Executed");
            }
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string requestedPath = context.HttpContext.Request.Path.ToString();
            
            ControllerActionName = context.HttpContext.Request.RouteValues["controller"].ToString() + "-" + context.HttpContext.Request.RouteValues["action"].ToString();

            if(!requestedPath.ToLower().Contains(path.ToLower()))
            {
                var headerValue = context.HttpContext.Request.Headers["Authorization"].ToString().Split(" ");

                var tokenString = headerValue[headerValue.Length - 1];
                
                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.ReadToken(tokenString) as JwtSecurityToken;

                var userId = token.Claims.FirstOrDefault(type => type.Type == ClaimTypes.UserData).Value;
                context.HttpContext.Items["userId"] = userId;

                Log.ForContext("userId", context.HttpContext.Items["userId"])
                .ForContext("Location", ControllerActionName)
                .Information($"{context.ActionDescriptor.DisplayName} is Starting...");
            }
            else
            {
                Log
                .ForContext("Location", ControllerActionName)
                .Information($"{context.ActionDescriptor.DisplayName} is Starting...");
            }
            
            
           
        }
    }
}
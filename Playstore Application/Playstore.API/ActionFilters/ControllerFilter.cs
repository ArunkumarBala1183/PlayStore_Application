using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Playstore.ActionFilters
{
    public class ControllerFilter : IActionFilter
    {
        private string ControllerActionName;
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Log.ForContext("userId" , context.HttpContext.Items["userId"])
            .ForContext("Location" , ControllerActionName)
            .Information($"{context.ActionDescriptor.DisplayName} is Executed");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            ControllerActionName = context.HttpContext.Request.RouteValues["controller"].ToString() + "-" + context.HttpContext.Request.RouteValues["action"].ToString();
            
            Log.ForContext("userId" , context.HttpContext.Items["userId"])
            .ForContext("Location" , ControllerActionName)
            .Information($"{context.ActionDescriptor.DisplayName} is Starting...");
        }
    }
}
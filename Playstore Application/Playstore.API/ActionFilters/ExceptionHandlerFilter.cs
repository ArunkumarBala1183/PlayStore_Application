using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Playstore.Core.Exceptions;

namespace Playstore.ActionFilters
{
    public class ExceptionHandlerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is ApiResponseException)
            {
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
                context.HttpContext.Response.WriteAsJsonAsync(new {message = context.Exception.Message});
                context.HttpContext.Items["ExceptionHandled"] = true;
            }
        }
    }
}
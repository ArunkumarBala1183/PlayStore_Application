using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Playstore.Core.Exceptions;


namespace Playstore
{
    public class GlobalExceptionHandlerMiddler : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddler> _logger;
        public GlobalExceptionHandlerMiddler(ILogger<GlobalExceptionHandlerMiddler> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var traceId = Guid.NewGuid();
                _logger.LogError($"Error occurred while processing the request: TraceId : {traceId}, Message : {exception.Message}, StackTrace: {exception.StackTrace}");

                int statusCode;
                string title;

                if (exception is ApiResponseException)
                {
                    statusCode = StatusCodes.Status500InternalServerError;
                    title = "Api Internal Server Error";
                }
                else if(exception is SqlException)
                {
                    statusCode = StatusCodes.Status500InternalServerError;
                    title = "Sql Exception Error";

                }
                else if (exception is EntityNotFoundException)
                {
                    statusCode = StatusCodes.Status404NotFound;
                    title = "Not Found";
                }
                else if (exception is InvalidRequestBodyException)
                {
                    statusCode = StatusCodes.Status400BadRequest;
                    title = "Bad Request";
                }
                else if (exception is ObjectNullException)
                {
                    statusCode = StatusCodes.Status500InternalServerError;
                    title = "No Object Found";
                }
                else if(exception is SpecificException)
                {
                     statusCode = StatusCodes.Status500InternalServerError;
                     title = "Internal Exception";

                }
                else
                {
                    statusCode = StatusCodes.Status500InternalServerError;
                    title = "Internal Server Error";
                }

                context.Response.StatusCode = statusCode;

                var problemDetails = GenerateProblemDetails(context.Request.Path, statusCode, title, traceId);

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }

        private ProblemDetails GenerateProblemDetails(string path, int statusCode, string title, Guid traceId)
        {
            return new ProblemDetails
            {
                Title = title,
                Status = statusCode,
                Instance = path,
                Detail = $"{title}, traceId : {traceId}"
            };
        }
    }
}
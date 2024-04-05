using System.Net;
using Microsoft.AspNetCore.Http;
using Playstore.Contracts.DTO.AppInfo;
using Serilog;

namespace Playstore.Core.Exceptions
{
    public class GlobalExceptionHandler<T>
    {
        private readonly ILogger logger;
        private readonly T repository;
        public GlobalExceptionHandler(T repository , IHttpContextAccessor httpContext)
        {
            this.repository = repository;
            logger = Log.ForContext("userId" , httpContext.HttpContext?.Items["userId"])
                        .ForContext("Location" , typeof(GlobalExceptionHandler<T>).Name);
        }
        public async Task<TResult> ManageException<TResult>(Func<T, TResult> method)
        {
            var message = string.Empty;
            try
            {
                var response = await Task.FromResult(method(repository));

                if(response is not HttpStatusCode)
                {
                    return response;
                }

                message = nameof(HttpStatusCode.NotFound);
            }
            catch (SqlException error)
            {
                logger.Error(error , error.Message);
                message = nameof(HttpStatusCode.ServiceUnavailable);
            }
            catch (Exception error)
            {
                logger.Error(error , error.Message);
                message = nameof(HttpStatusCode.InternalServerError);
            }
            
            throw new ApiResponseException(message);
        }            
    }
}
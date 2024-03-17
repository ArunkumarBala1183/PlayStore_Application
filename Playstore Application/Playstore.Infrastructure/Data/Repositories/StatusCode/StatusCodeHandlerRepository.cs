using System.Net;
using Playstore.Contracts.Data.Repositories;
using Playstore.Core.Exceptions;

namespace Playstore.Core.Data.Repositories.StatusCode
{
    public class StatusCodeHandlerRepository : IStatusCodeHandlerRepository
    {
        public void HandleStatusCode(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.NotFound : 

                throw new ApiResponseException("No Records Found");
                
                case HttpStatusCode.ServiceUnavailable:

                throw new ApiResponseException("Server Unavailable");

                case HttpStatusCode.InternalServerError:

                throw new ApiResponseException("Internal Server Error");

                case HttpStatusCode.BadRequest:

                throw new ApiResponseException("Insert Correct Details");

                default:

                throw new ApiResponseException("Something Went Wrong");
            }
        }
    }
}
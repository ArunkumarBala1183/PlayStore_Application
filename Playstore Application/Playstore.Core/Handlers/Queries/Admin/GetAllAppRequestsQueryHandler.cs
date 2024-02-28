using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppRequests;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetAllAppRequestsQuery : IRequest<IEnumerable<RequestDetailsDto>>
    {
    }

    public class GetAllAppRequestsQueryHandler : IRequestHandler<GetAllAppRequestsQuery, IEnumerable<RequestDetailsDto>>
    {
        private readonly IAppRequestsRepository repository;
        private readonly IStatusCodeHandlerRepository statusCodeHandler;
        public GetAllAppRequestsQueryHandler(IAppRequestsRepository repository , IStatusCodeHandlerRepository statusCodeHandler)
        {
            this.repository = repository;
            this.statusCodeHandler = statusCodeHandler;
        }
        public async Task<IEnumerable<RequestDetailsDto>> Handle(GetAllAppRequestsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await this.repository.GetAllRequests();
    
                if(response.GetType() == typeof(HttpStatusCode))
                {
                    statusCodeHandler.HandleStatusCode((HttpStatusCode) response);
                }
    
                return (IEnumerable<RequestDetailsDto>) response;
            }
            catch (ApiResponseException)
            {
                throw;
            }

        }
    }
}
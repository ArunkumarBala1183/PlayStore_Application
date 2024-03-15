using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppRequests;

namespace Playstore.Providers.Handlers.Queries.Admin
{
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
            var response = await this.repository.GetAllRequests();

            if (response is HttpStatusCode code)
            {
                statusCodeHandler.HandleStatusCode(code);
            }

            return (IEnumerable<RequestDetailsDto>)response;

        }
    }
}
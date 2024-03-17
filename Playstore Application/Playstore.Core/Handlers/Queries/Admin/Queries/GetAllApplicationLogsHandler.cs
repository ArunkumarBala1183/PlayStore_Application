using System.Net;
using MediatR;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppRequests;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetAllApplicationLogsHandler : IRequestHandler<GetAllApplicationLogsQuery, IEnumerable<AppLog>>
    {
        private readonly IApplicationLogsRepository repository;
        private readonly IStatusCodeHandlerRepository statusCodeHandler;
        public GetAllApplicationLogsHandler(IApplicationLogsRepository repository , IStatusCodeHandlerRepository statusCodeHandler)
        {
            this.repository = repository;
            this.statusCodeHandler = statusCodeHandler;
        }
        public async Task<IEnumerable<AppLog>> Handle(GetAllApplicationLogsQuery request, CancellationToken cancellationToken)
        {
            var response = await this.repository.ViewLogs();

            if (response is HttpStatusCode code)
            {
                statusCodeHandler.HandleStatusCode(code);
            }

            return (IEnumerable<AppLog>)response;

        }
    }
}
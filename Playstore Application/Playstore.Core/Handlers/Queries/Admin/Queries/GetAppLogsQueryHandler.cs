using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppDownloads;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetAppLogsQueryHandler : IRequestHandler<GetAppLogsQuery, IEnumerable<AppDownloadsDto>>
    {
        private readonly IAppDownloadsRepository repository;
        private readonly IStatusCodeHandlerRepository statusCodeHandler;
        public GetAppLogsQueryHandler(IAppDownloadsRepository repository , IStatusCodeHandlerRepository statusCodeHandler)
        {
            this.repository = repository;
            this.statusCodeHandler = statusCodeHandler;
        }
        public async Task<IEnumerable<AppDownloadsDto>> Handle(GetAppLogsQuery request, CancellationToken cancellationToken)
        {
            var response = await this.repository.GetAppLogs(request.AppSearch);

            if (response is HttpStatusCode code)
            {
                statusCodeHandler.HandleStatusCode(code);
            }

            return (IEnumerable<AppDownloadsDto>)response;
        }
    }
}
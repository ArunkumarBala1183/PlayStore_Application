using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppData;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Queries.Admin
{

    public class GetRequestedAppDataQueryHandler : IRequestHandler<GetRequestedAppDataQuery, RequestedAppDataDto>
    {
        private readonly IAppDataRepository repository;
        private readonly IStatusCodeHandlerRepository statusCodeHandler;
        public GetRequestedAppDataQueryHandler(IAppDataRepository repository , IStatusCodeHandlerRepository statusCodeHandler)
        {
            this.repository = repository;
            this.statusCodeHandler = statusCodeHandler;
        }
        public async Task<RequestedAppDataDto> Handle(GetRequestedAppDataQuery request, CancellationToken cancellationToken)
        {
            var response = await this.repository.GetAppData(request.AppId);

            if (response is HttpStatusCode code)
            {
                statusCodeHandler.HandleStatusCode(code);
            }

            return (RequestedAppDataDto)response;
        }
    }
}
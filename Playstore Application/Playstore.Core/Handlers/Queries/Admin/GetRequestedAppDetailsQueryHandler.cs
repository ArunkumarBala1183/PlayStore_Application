using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppInfo;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetRequestedAppDetailsQuery : IRequest<RequestAppInfoDto>
    {
        public Guid appId;
        public GetRequestedAppDetailsQuery(Guid appId)
        {
            this.appId = appId;
        }
    }

    public class GetRequestedAppDetailsQueryHandler : IRequestHandler<GetRequestedAppDetailsQuery, RequestAppInfoDto>
    {
        private readonly IAppRequestsRepository repository;
        private readonly IStatusCodeHandlerRepository statusCodeHandler;
        public GetRequestedAppDetailsQueryHandler(IAppRequestsRepository repository , IStatusCodeHandlerRepository statusCodeHandler)
        {
            this.repository = repository;
            this.statusCodeHandler = statusCodeHandler;
        }
        public async Task<RequestAppInfoDto> Handle(GetRequestedAppDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var appDetails = await this.repository.GetRequestedAppDetails(request.appId);

                if(appDetails.GetType() == typeof(HttpStatusCode))
                {
                    statusCodeHandler.HandleStatusCode((HttpStatusCode) appDetails);
                }

                return (RequestAppInfoDto) appDetails;
            }
            catch (ApiResponseException)
            {
                throw;
            }
        }
    }
}
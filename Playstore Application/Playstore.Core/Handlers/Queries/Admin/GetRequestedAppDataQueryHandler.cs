using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppData;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetRequestedAppDataQuery : IRequest<RequestedAppDataDto>
    {
        public Guid appId;
        public GetRequestedAppDataQuery(Guid appId)
        {
            this.appId = appId;
        }
    }

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
            try
            {
                var response = await this.repository.GetAppData(request.appId);
    
                if(response.GetType() == typeof(HttpStatusCode))
                {
                    statusCodeHandler.HandleStatusCode((HttpStatusCode) response);
                }
    
                return (RequestedAppDataDto) response;
            }
            catch (ApiResponseException)
            {
                
                throw;
            }
        }
    }
}
using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppInfo;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetAllAppsInfoQueryHandler : IRequestHandler<GetAllAppsInfoQuery, IEnumerable<ListAppInfoDto>>
    {
        private readonly IAppInfoRepository _repository;
        private readonly IStatusCodeHandlerRepository statusCodeHandler;
        public GetAllAppsInfoQueryHandler(IAppInfoRepository repository , IStatusCodeHandlerRepository statusCodeHandler)
        {
            this._repository = repository;
            this.statusCodeHandler = statusCodeHandler;
        }
        public async Task<IEnumerable<ListAppInfoDto>> Handle(GetAllAppsInfoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await this._repository.ViewAllApps(request.UserId);
    
                if(response.GetType() == typeof(HttpStatusCode))
                {
                    statusCodeHandler.HandleStatusCode((HttpStatusCode) response);
                }
    
                return (IEnumerable<ListAppInfoDto>) response;
            }
            catch (ApiResponseException)
            {
                
                throw;
            }
        }
    }
}
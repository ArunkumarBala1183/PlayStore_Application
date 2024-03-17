using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppInfo;
using Playstore.Core.Exceptions;
using Serilog;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetAllAppsInfoQueryHandler : IRequestHandler<GetAllAppsInfoQuery, IEnumerable<ListAppInfoDto>>
    {
        private readonly IAppInfoRepository _repository;
        private readonly IStatusCodeHandlerRepository statusCodeHandler;
        private readonly IHttpContextAccessor httpContext;
        public GetAllAppsInfoQueryHandler(IAppInfoRepository repository , IStatusCodeHandlerRepository statusCodeHandler , IHttpContextAccessor httpContext)
        {
            this._repository = repository;
            this.statusCodeHandler = statusCodeHandler;
            this.httpContext = httpContext;
        }
        public async Task<IEnumerable<ListAppInfoDto>> Handle(GetAllAppsInfoQuery request, CancellationToken cancellationToken)
        {
            var response = await this._repository.ViewAllApps(request.UserId);

            if (response is HttpStatusCode code)
            {
                statusCodeHandler.HandleStatusCode(code);
            }

            return (IEnumerable<ListAppInfoDto>)response;
        }
    }
}
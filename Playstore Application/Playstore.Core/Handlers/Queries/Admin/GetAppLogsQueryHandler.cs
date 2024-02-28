using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppDownloads;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetAppLogsQuery : IRequest<IEnumerable<AppDownloadsDto>>
    {
        public AppLogsDto appSearch { get; set; }
        public GetAppLogsQuery(AppLogsDto appLogsDto)
        {
            this.appSearch = appLogsDto;
        }
    }

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
            try
            {
                var response =  await this.repository.GetAppLogs(request.appSearch);
    
                if(response.GetType() == typeof(HttpStatusCode))
                {
                    statusCodeHandler.HandleStatusCode((HttpStatusCode) response);
                }
    
                return (IEnumerable<AppDownloadsDto>) response;
            }
            catch (ApiResponseException)
            {
                
                throw;
            }
        }
    }
}
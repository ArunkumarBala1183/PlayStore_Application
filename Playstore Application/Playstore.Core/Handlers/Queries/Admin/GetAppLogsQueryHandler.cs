using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.AppDownloads;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetAppLogsQuery : IRequest<object>
    {
        public AppLogsDto appSearch { get; set; }
        public GetAppLogsQuery(AppLogsDto appLogsDto)
        {
            this.appSearch = appLogsDto;
        }
    }

    public class GetAppLogsQueryHandler : IRequestHandler<GetAppLogsQuery, object>
    {
        private readonly IAppDownloadsRepository repository;
        public GetAppLogsQueryHandler(IAppDownloadsRepository repository)
        {
            this.repository = repository;
        }
        public async Task<object> Handle(GetAppLogsQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetAppLogs(request.appSearch);
        }
    }
}
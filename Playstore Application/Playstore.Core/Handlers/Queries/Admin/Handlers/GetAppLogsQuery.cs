using MediatR;
using Playstore.Contracts.DTO.AppDownloads;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetAppLogsQuery : IRequest<IEnumerable<AppDownloadsDto>>
    {
        public AppLogsDto AppSearch { get; set; }
        public GetAppLogsQuery(AppLogsDto appLogsDto)
        {
            this.AppSearch = appLogsDto;
        }
    }
}
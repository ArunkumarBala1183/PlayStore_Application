using MediatR;
using Playstore.Contracts.DTO.TotalDownloads;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetTotalDownloadsQuery : IRequest<DownloadDetailsDto>
    {

    }
}
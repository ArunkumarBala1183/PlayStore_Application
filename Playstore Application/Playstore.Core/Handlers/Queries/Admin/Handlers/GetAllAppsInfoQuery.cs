using MediatR;
using Playstore.Contracts.DTO.AppInfo;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetAllAppsInfoQuery : IRequest<IEnumerable<ListAppInfoDto>>
    {}
}
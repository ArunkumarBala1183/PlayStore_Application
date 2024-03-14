using MediatR;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO.AppRequests;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetAllApplicationLogsQuery : IRequest<IEnumerable<AppLog>>
    {
        
    }
}
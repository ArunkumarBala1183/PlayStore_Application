using MediatR;
using Playstore.Contracts.DTO.AppRequests;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetAllAppRequestsQuery : IRequest<IEnumerable<RequestDetailsDto>>
    {
    }

}
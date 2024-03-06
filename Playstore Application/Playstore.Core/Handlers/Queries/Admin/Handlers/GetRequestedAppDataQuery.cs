using MediatR;
using Playstore.Contracts.DTO.AppData;

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
}
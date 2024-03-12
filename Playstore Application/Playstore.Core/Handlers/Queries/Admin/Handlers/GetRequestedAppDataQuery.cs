using MediatR;
using Playstore.Contracts.DTO.AppData;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetRequestedAppDataQuery : IRequest<RequestedAppDataDto>
    {
        public Guid AppId { get; set; }
        public GetRequestedAppDataQuery(Guid appId)
        {
            this.AppId = appId;
        }
    }
}
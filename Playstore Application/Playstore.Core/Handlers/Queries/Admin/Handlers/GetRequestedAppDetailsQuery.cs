using MediatR;
using Playstore.Contracts.DTO.AppInfo;

namespace Playstore.Providers.Handlers.Queries.Admin
{
     public class GetRequestedAppDetailsQuery : IRequest<RequestAppInfoDto>
    {
        public Guid AppId {get ; set; }
        public GetRequestedAppDetailsQuery(Guid appId)
        {
            this.AppId = appId;
        }
    }
}
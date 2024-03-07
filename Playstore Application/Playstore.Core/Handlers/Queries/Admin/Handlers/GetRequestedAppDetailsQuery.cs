using MediatR;
using Playstore.Contracts.DTO.AppInfo;

namespace Playstore.Providers.Handlers.Queries.Admin
{
     public class GetRequestedAppDetailsQuery : IRequest<RequestAppInfoDto>
    {
        public Guid appId;
        public GetRequestedAppDetailsQuery(Guid appId)
        {
            this.appId = appId;
        }
    }
}
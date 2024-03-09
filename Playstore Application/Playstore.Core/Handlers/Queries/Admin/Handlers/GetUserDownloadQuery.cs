using MediatR;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetUserDownloadQuery : IRequest<bool>
    {
        public Guid AppId { get; set; }
        public Guid UserId { get; set; }
    }
}
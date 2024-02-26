using MediatR;
using Playstore.Contracts.Data.Repositories;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetRequestedAppDetailsQuery : IRequest<object>
    {
        public Guid appId;
        public GetRequestedAppDetailsQuery(Guid appId)
        {
            this.appId = appId;
        }
    }

    public class GetRequestedAppDetailsQueryHandler : IRequestHandler<GetRequestedAppDetailsQuery, object>
    {
        private readonly IAppRequestsRepository repository;
        public GetRequestedAppDetailsQueryHandler(IAppRequestsRepository repository)
        {
            this.repository = repository;
        }
        public async Task<object> Handle(GetRequestedAppDetailsQuery request, CancellationToken cancellationToken)
        {
            var appDetails = await this.repository.GetRequestedAppDetails(request.appId);

            return appDetails;
        }
    }
}
using MediatR;
using Playstore.Contracts.Data.Repositories;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetRequestedAppDataQuery : IRequest<object>
    {
        public Guid appId;
        public GetRequestedAppDataQuery(Guid appId)
        {
            this.appId = appId;
        }
    }

    public class GetRequestedAppDataQueryHandler : IRequestHandler<GetRequestedAppDataQuery, object>
    {
        private readonly IAppDataRepository repository;
        public GetRequestedAppDataQueryHandler(IAppDataRepository repository)
        {
            this.repository = repository;
        }
        public async Task<object> Handle(GetRequestedAppDataQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetAppData(request.appId);
        }
    }
}
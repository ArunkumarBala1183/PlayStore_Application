using MediatR;
using Playstore.Contracts.Data.Repositories;

namespace Playstore.Providers.Handlers.Queries
{
    public class GetAllAppsInfoQuery : IRequest<object>
    {}

    public class GetAllAppsInfoQueryHandler : IRequestHandler<GetAllAppsInfoQuery, object>
    {
        private readonly IAppInfoRepository _repository;
        public GetAllAppsInfoQueryHandler(IAppInfoRepository repository)
        {
            this._repository = repository;
        }
        public Task<object> Handle(GetAllAppsInfoQuery request, CancellationToken cancellationToken)
        {
            return this._repository.ViewAllApps();
        }
    }
}
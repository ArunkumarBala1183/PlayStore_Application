using MediatR;
using Playstore.Contracts.Data.Repositories;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetAllAppsDownloadsQuery : IRequest<object>
    {
        
    }

    public class GetAllAppsDownloadsQueryHandler : IRequestHandler<GetAllAppsDownloadsQuery, object>
    {
        private readonly IAppDownloadsRepository repository;
        public GetAllAppsDownloadsQueryHandler(IAppDownloadsRepository repository)
        {
            this.repository = repository;
        }
        public async Task<object> Handle(GetAllAppsDownloadsQuery request, CancellationToken cancellationToken)
        {
            return await this.repository.GetAllAppDownloads();
        }
    }
}
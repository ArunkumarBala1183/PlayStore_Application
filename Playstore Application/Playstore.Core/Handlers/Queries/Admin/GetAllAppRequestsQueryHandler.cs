using MediatR;
using Playstore.Contracts.Data.Repositories;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetAllAppRequestsQuery : IRequest<object>
    {
    }

    public class GetAllAppRequestsQueryHandler : IRequestHandler<GetAllAppRequestsQuery, object>
    {
        private readonly IAppRequestsRepository repository;
        public GetAllAppRequestsQueryHandler(IAppRequestsRepository repository)
        {
            this.repository = repository;
        }
        public async Task<object> Handle(GetAllAppRequestsQuery request, CancellationToken cancellationToken)
        {
            var response = await this.repository.GetAllRequests();

            return response;
        }
    }
}
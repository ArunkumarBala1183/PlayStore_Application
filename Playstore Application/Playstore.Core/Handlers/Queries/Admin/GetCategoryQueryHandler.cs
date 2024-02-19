using MediatR;
using Playstore.Contracts.Data.Repositories;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetCategoryQuery : IRequest<object>
    {
        public Guid id;
        public GetCategoryQuery(Guid id)
        {
            this.id = id;
        }   
    }
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, object>
    {
        private readonly ICategoryRepository repository;

        public GetCategoryQueryHandler(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<object> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var response = await this.repository.GetCategory(request.id);

            return response;
        }
    }
}
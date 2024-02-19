using MediatR;
using Playstore.Contracts.Data.Repositories;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetAllCategoryQuery : IRequest<object>
    {
    }
    public class GetAllCategoryQueryHandler: IRequestHandler<GetAllCategoryQuery, object>
    {
        private readonly ICategoryRepository repository;
        public GetAllCategoryQueryHandler(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<object> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var response = await this.repository.GetAllCategory();
            return response;
        }
    }
}
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.Category;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class SearchCategoryQuery : IRequest<object>
    {
        public CategoryDto category;
        public SearchCategoryQuery(CategoryDto category)
        {
            this.category = category;
        }
    }
    public class SearchCategoryQueryHandler : IRequestHandler<SearchCategoryQuery, object>
    {
        private readonly ICategoryRepository repository;
        public SearchCategoryQueryHandler(ICategoryRepository repository)
        {
            this.repository = repository;
        }
        public async Task<object> Handle(SearchCategoryQuery request, CancellationToken cancellationToken)
        {
            var response = await this.repository.SearchCategory(request.category);

            return response;
        }
    }
}
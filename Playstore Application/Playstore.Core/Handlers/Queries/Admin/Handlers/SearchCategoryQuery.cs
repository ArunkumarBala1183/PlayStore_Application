using MediatR;
using Playstore.Contracts.DTO.Category;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class SearchCategoryQuery : IRequest<IEnumerable<string>>
    {
        public CategoryDto category;
        public SearchCategoryQuery(CategoryDto category)
        {
            this.category = category;
        }
    }
}
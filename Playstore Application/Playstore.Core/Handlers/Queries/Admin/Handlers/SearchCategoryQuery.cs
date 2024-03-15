using MediatR;
using Playstore.Contracts.DTO.Category;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class SearchCategoryQuery : IRequest<IEnumerable<string>>
    {
        public CategoryDto Category {get ; set ;}
        public SearchCategoryQuery(CategoryDto category)
        {
            this.Category = category;
        }
    }
}
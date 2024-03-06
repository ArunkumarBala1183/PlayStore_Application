using MediatR;
using Playstore.Contracts.DTO.Category;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetCategoryQuery : IRequest<CategoryUpdateDto>
    {
        public Guid id;
        public GetCategoryQuery(Guid id)
        {
            this.id = id;
        }   
    }
}
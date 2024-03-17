using MediatR;
using Playstore.Contracts.DTO.Category;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetCategoryQuery : IRequest<CategoryUpdateDto>
    {
        public Guid Id { get; set; }
        public GetCategoryQuery(Guid id)
        {
            this.Id = id;
        }   
    }
}
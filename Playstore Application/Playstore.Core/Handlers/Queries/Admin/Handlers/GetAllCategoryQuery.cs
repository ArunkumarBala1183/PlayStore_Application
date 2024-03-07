using MediatR;
using Playstore.Contracts.DTO.Category;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetAllCategoryQuery : IRequest<IEnumerable<CategoryUpdateDto>>
    {
    }
}
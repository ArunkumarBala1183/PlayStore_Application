using System.Net;
using MediatR;
using Playstore.Contracts.DTO.Category;

namespace Playstore.Providers.Handlers.Commands
{
    public class UpdateCategoryCommand : IRequest<HttpStatusCode>
    {
        public CategoryUpdateDto category;
        public UpdateCategoryCommand(CategoryUpdateDto category)
        {
            this.category = category;
        }
    }
}
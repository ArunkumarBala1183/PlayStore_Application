using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;
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

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, HttpStatusCode>
    {
        private readonly ICategoryRepository repository;
        public UpdateCategoryCommandHandler(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<HttpStatusCode> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = await this.repository.UpdateCategory(request.category);

            return response;
        }
    }
}
using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.Category;
using Playstore.Core.Exceptions;

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
        private readonly IStatusCodeHandlerRepository statusCodeHandler;
        public UpdateCategoryCommandHandler(ICategoryRepository repository, IStatusCodeHandlerRepository statusCodeHandler)
        {
            this.repository = repository;
            this.statusCodeHandler = statusCodeHandler;
        }

        public async Task<HttpStatusCode> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await this.repository.UpdateCategory(request.category);

                if (response != HttpStatusCode.OK)
                {
                    statusCodeHandler.HandleStatusCode(response);
                }

                return response;
            }
            catch (ApiResponseException)
            {
                throw;
            }
        }
    }
}
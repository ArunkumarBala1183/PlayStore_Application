using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;

namespace Playstore.Providers.Handlers.Commands
{
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
            var response = await this.repository.UpdateCategory(request.Category);

            if (response != HttpStatusCode.OK)
            {
                statusCodeHandler.HandleStatusCode(response);
            }

            return response;
        }
    }
}
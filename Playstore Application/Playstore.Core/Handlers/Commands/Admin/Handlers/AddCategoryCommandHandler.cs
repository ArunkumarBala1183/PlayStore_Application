using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;

namespace Playstore.Providers.Handlers.Commands
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, HttpStatusCode>
    {
        private readonly ICategoryRepository repository;
        private readonly IStatusCodeHandlerRepository statusCodeHandler;
        public AddCategoryCommandHandler(ICategoryRepository repository , IStatusCodeHandlerRepository statusCodeHandler)
        {
            this.repository = repository;
            this.statusCodeHandler = statusCodeHandler;

        }
        public async Task<HttpStatusCode> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = await this.repository.AddCategory(request.Category);

            if (response != HttpStatusCode.AlreadyReported && response != HttpStatusCode.Created)
            {
                statusCodeHandler.HandleStatusCode(response);
            }

            return response;
        }
    }
}
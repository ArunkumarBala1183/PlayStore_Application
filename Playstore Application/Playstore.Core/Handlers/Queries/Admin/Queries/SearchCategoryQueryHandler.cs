using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.Category;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class SearchCategoryQueryHandler : IRequestHandler<SearchCategoryQuery, IEnumerable<string>>
    {
        private readonly ICategoryRepository repository;
        private readonly IStatusCodeHandlerRepository statusCodeHandler;
        public SearchCategoryQueryHandler(ICategoryRepository repository , IStatusCodeHandlerRepository statusCodeHandler)
        {
            this.repository = repository;
            this.statusCodeHandler = statusCodeHandler;

        }
        public async Task<IEnumerable<string>> Handle(SearchCategoryQuery request, CancellationToken cancellationToken)
        {
            var response = await this.repository.SearchCategory(request.Category);

            if (response is HttpStatusCode code)
            {
                statusCodeHandler.HandleStatusCode(code);
            }

            return (IEnumerable<string>)response;
        }
    }
}
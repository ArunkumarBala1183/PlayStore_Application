using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.Category;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class SearchCategoryQuery : IRequest<IEnumerable<string>>
    {
        public CategoryDto category;
        public SearchCategoryQuery(CategoryDto category)
        {
            this.category = category;
        }
    }
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
            try
            {
                var response = await this.repository.SearchCategory(request.category);
    
                if(response.GetType() == typeof(HttpStatusCode))
                {
                    statusCodeHandler.HandleStatusCode((HttpStatusCode) response);
                }
    
                return (IEnumerable<string>) response;
            }
            catch (ApiResponseException)
            {
                
                throw;
            }
        }
    }
}
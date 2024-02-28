using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.Category;
using Playstore.Core.Exceptions;

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
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryUpdateDto>
    {
        private readonly ICategoryRepository repository;
        private readonly IStatusCodeHandlerRepository statusCodeHandler;

        public GetCategoryQueryHandler(ICategoryRepository repository , IStatusCodeHandlerRepository statusCodeHandler)
        {
            this.repository = repository;
            this.statusCodeHandler = statusCodeHandler;
        }

        public async Task<CategoryUpdateDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await this.repository.GetCategory(request.id);

                if(response.GetType() == typeof(HttpStatusCode))
                {
                    statusCodeHandler.HandleStatusCode((HttpStatusCode) response);
                }
    
                return (CategoryUpdateDto) response;
            }
            catch (ApiResponseException)
            {
                throw;
            }
        }
    }
}
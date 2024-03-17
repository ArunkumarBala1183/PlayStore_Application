using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.Category;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, IEnumerable<CategoryUpdateDto>>
    {
        private readonly ICategoryRepository repository;
        private readonly IStatusCodeHandlerRepository statusCodeHandler;
        public GetAllCategoryQueryHandler(ICategoryRepository repository , IStatusCodeHandlerRepository statusCodeHandler)
        {
            this.repository = repository;
            this.statusCodeHandler= statusCodeHandler;
        }

        public async Task<IEnumerable<CategoryUpdateDto>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var response = await this.repository.GetAllCategory();

            if (response is HttpStatusCode code)
            {
                statusCodeHandler.HandleStatusCode(code);
            }

            return (IEnumerable<CategoryUpdateDto>)response;
        }
    }
}
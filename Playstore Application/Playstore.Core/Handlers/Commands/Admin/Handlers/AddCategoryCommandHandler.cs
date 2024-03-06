using System.Net;
using AutoMapper;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.Category;
using Playstore.Core.Exceptions;

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
            try
            {
                var response = await this.repository.AddCategory(request._category);

                if(response != HttpStatusCode.AlreadyReported && response != HttpStatusCode.Created)
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
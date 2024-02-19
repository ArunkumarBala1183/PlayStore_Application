using System.Net;
using AutoMapper;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.Category;

namespace Playstore.Providers.Handlers.Commands
{
    public class AddCategoryCommand : IRequest<HttpStatusCode>
    {
        public CategoryDto _category;
        public AddCategoryCommand(CategoryDto category)
        {
            this._category = category;
        }  
    }
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, HttpStatusCode>
    {
        private readonly ICategoryRepository repository;
        public AddCategoryCommandHandler(ICategoryRepository repository)
        {
            this.repository = repository;
        }
        public async Task<HttpStatusCode> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = await this.repository.AddCategory(request._category);

            return response;
        }
    }
}
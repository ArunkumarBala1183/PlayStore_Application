using MediatR;
using Playstore.Contracts.DTO;
using Playstore.Contracts.Data.Entities;
using Playstore.Core.Exceptions;
using AutoMapper;
using Playstore.Contracts.Data;


namespace Playstore.Providers.Handlers.Queries.UserData
{
    public class GetAllCategory : IRequest<Guid>
    {
        public string AppCategory { get; }
        public GetAllCategory(string _AppCategory)
        {
            AppCategory = _AppCategory;
        }
    }

    public class GetAllCategoryHandler : IRequestHandler<GetAllCategory, Guid>
    {
        private readonly IUnitOfWork _repository;


        public GetAllCategoryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;

        }

        public async Task<Guid> Handle(GetAllCategory request, CancellationToken cancellationToken)
        {

            var category = await Task.FromResult(_repository.GetCategory.GetAll().FirstOrDefault(c => c.CategoryName == request.AppCategory));
            if (category == null)

            {
                throw new EntityNotFoundException($"No App found for Id");// You can throw an exception or handle the case where the category is not found.
            }
            return category.CategoryId;
        }


    }
}
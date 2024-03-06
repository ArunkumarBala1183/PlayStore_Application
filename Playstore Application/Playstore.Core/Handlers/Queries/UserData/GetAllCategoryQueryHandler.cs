using AutoMapper;
using MediatR;
using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.Category;

namespace Playstore.Providers.Handlers.Queries.UserData;

public class GetAllCategoryQuery : IRequest<IEnumerable<Category>>
{

}

public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, IEnumerable<Category>>
{
    private IUnitOfWork _repository;

    public GetAllCategoryQueryHandler(IUnitOfWork repository)
    {
        _repository = repository;

    }

    public async Task<IEnumerable<Category>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await Task.FromResult(_repository.GetCategory.GetAll().ToList());


        return (IEnumerable<Category>)category;
    }
}



using AutoMapper;
using MediatR;
using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Repositories;

namespace Playstore.Providers.Handlers.Queries.UserData;

public class GetAllCategoryQuery : IRequest<object>
{

}

public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, object>
{
    private IUnitOfWork _repository;
    public GetAllCategoryQueryHandler(IUnitOfWork repository)
    {
        _repository = repository;
    }

    public async Task<object> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await _repository.CategoryDetails.GetAllCategory();
        return category;
    }
}



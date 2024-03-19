using MediatR;
using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Utility;
using Playstore.Core.Exceptions;


namespace Playstore.Providers.Handlers.Queries.UserData;

public class GetAllCategoryQuery : IRequest<IEnumerable<Category>>
{

}

public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, IEnumerable<Category>>
{
    private readonly IUnitOfWork _repository;

    public GetAllCategoryQueryHandler(IUnitOfWork repository)
    {
        _repository = repository;

    }

    public async Task<IEnumerable<Category>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await Task.FromResult(_repository.GetCategory.GetAll().ToList());
        
        if(category==null)
        {
             throw new EntityNotFoundException(Dataconstant.EntityNotFoundException);
        }


        return category;
    }
}



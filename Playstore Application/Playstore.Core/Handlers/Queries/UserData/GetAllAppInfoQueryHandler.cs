using AutoMapper;
using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO;
using MediatR;
using System.Linq;
using Playstore.Core.Exceptions;
using Playstore.Contracts.Data.Utility;

namespace Playstore.Providers.Handlers.Queries.UserData
{
    public class GetAllAppInfoQuery : IRequest<IEnumerable<AppsdetailsDTO>>
    {
    }

    public class GetAllAppInfoQueryHandler : IRequestHandler<GetAllAppInfoQuery, IEnumerable<AppsdetailsDTO>>
    {
        private readonly IUnitOfWork _repository;


        public GetAllAppInfoQueryHandler(IUnitOfWork repository)
        {
            _repository = repository;

        }

        public async Task<IEnumerable<AppsdetailsDTO>> Handle(GetAllAppInfoQuery request, CancellationToken cancellationToken)
        {

            var app = await _repository.AppValue.ViewAllApps();
            
            if (app == null)
            {
                throw new EntityNotFoundException(Dataconstant.EntityNotFoundException);
            }

            return (IEnumerable<AppsdetailsDTO>)app;
        }
    }
}
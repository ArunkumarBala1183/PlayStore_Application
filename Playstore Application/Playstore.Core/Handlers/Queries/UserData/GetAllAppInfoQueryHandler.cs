using AutoMapper;
using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO;
using MediatR;
using System.Linq;

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
            return (IEnumerable<AppsdetailsDTO>)app;
        }
    }
}
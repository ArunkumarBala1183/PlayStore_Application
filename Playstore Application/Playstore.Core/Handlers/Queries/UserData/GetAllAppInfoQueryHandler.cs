using AutoMapper;
using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO;
using MediatR;
using System.Linq;

namespace Playstore.Providers.Handlers.Queries.UserData
{
    public class GetAllAppInfoQuery : IRequest<object>
    {
    }

    public class GetAllAppInfoQueryHandler : IRequestHandler<GetAllAppInfoQuery, object>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllAppInfoQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetAllAppInfoQuery request, CancellationToken cancellationToken)
        {
           var app=await _repository.AppValue.ViewAllApps();
           return app;
        }
    }
}
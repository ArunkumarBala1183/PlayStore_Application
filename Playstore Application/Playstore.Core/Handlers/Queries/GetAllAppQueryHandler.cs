using AutoMapper;
using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO;
using MediatR;
using System.Linq;
using Playstore.Contracts.Data.Repositories;

namespace Playstore.Providers.Handlers.Queries
{
    public class GetAllAppQuery : IRequest<IEnumerable<AppDTO>>
    {
    }

    public class GetAllAppQueryHandler : IRequestHandler<GetAllAppQuery, IEnumerable<AppDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllAppQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppDTO>> Handle(GetAllAppQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(_repository.App.GetAll());
            return _mapper.Map<IEnumerable<AppDTO>>(entities);
        }
    }
}

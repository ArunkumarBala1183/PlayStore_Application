using AutoMapper;
using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO;
using MediatR;
using System.Linq;

namespace Playstore.Providers.Handlers.Queries
{
    public class GetAllUserQuery : IRequest<IEnumerable<UserDTO>>
    {
    }

    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, IEnumerable<UserDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllUserQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(_repository.User.GetAll());
            return _mapper.Map<IEnumerable<UserDTO>>(entities);
        }
    }
}
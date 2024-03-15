
using AutoMapper;
using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.DTO;
using MediatR;
using System.Linq;
using Playstore.Contracts.Data.Repositories;

namespace Playstore.Providers.Handlers.Queries
{
    public class GetAllUsersQuery : IRequest<object>
    {
        public Guid Id { get; set; }
        public GetAllUsersQuery(Guid id)
        {
            this.Id = id;
        }
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, object>
    {
        private readonly IUsersRepository _repository;

        public GetAllUsersQueryHandler(IUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAll(request.Id);

            return entities;
        }
    }
}
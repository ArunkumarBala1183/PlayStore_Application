using MediatR;
using Playstore.Contracts.Data.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Playstore.Providers.Handlers.Queries
{

    public class CheckEmailExistenceQuery : IRequest<bool>
    {
        public string Email { get; }

        public CheckEmailExistenceQuery(string email)
        {
            Email = email;
        }
    }
    public class CheckEmailExistenceQueryHandler : IRequestHandler<CheckEmailExistenceQuery, bool>
    {
        private readonly IUsersRepository _usersRepository;

        public CheckEmailExistenceQueryHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<bool> Handle(CheckEmailExistenceQuery request, CancellationToken cancellationToken)
        {
            var existingUser = await _usersRepository.GetByEmailId(request.Email);
            return existingUser != null;
        }
    }
}

using MediatR;
using Playstore.Contracts.Data.Repositories;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetAllUsersInfoQuery : IRequest<object>
    {
        
    }

    public class GetAllUsersInfoQueryHandler : IRequestHandler<GetAllUsersInfoQuery, object>
    {
        private readonly IUserInfoRepository repository;
        public GetAllUsersInfoQueryHandler(IUserInfoRepository users)
        {
            this.repository = users;
        }
        public async Task<object> Handle(GetAllUsersInfoQuery request, CancellationToken cancellationToken)
        {
            var response = await this.repository.ViewAllUsers();

            return response;
        }
    }
}
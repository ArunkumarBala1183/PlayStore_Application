using MediatR;
using Playstore.Contracts.DTO.UserInfo;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetAllUsersInfoQuery : IRequest<IEnumerable<UserInfoDto>>
    {
        
    }
}
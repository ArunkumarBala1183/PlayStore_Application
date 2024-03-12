using MediatR;
using Playstore.Contracts.DTO.UserInfo;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetUserDetailsQuery : IRequest<IEnumerable<UserInfoDto>>
    {
        public string? SearchDetails { get; set; }
    }
}
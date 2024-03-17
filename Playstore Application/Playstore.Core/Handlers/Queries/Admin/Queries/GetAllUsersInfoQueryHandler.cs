using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.UserInfo;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Queries.Admin
{

    public class GetAllUsersInfoQueryHandler : IRequestHandler<GetAllUsersInfoQuery, IEnumerable<UserInfoDto>>
    {
        private readonly IUserInfoRepository repository;
        private readonly IStatusCodeHandlerRepository statusCodeHandler;
        public GetAllUsersInfoQueryHandler(IUserInfoRepository users , IStatusCodeHandlerRepository statusCodeHandler)
        {
            this.repository = users;
            this.statusCodeHandler = statusCodeHandler;
        }
        public async Task<IEnumerable<UserInfoDto>> Handle(GetAllUsersInfoQuery request, CancellationToken cancellationToken)
        {
            var response = await this.repository.ViewAllUsers();

            if (response is HttpStatusCode code)
            {
                statusCodeHandler.HandleStatusCode(code);
            }

            return (IEnumerable<UserInfoDto>)response;
        }
    }
}
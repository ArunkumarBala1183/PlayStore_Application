using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO.UserInfo;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, IEnumerable<UserInfoDto>>
    {
        private readonly IUserInfoRepository repository;
        private readonly IStatusCodeHandlerRepository statusCodeHandler;

        public GetUserDetailsQueryHandler(IUserInfoRepository repository , IStatusCodeHandlerRepository statusCodeHandler)
        {
            this.repository = repository;
            this.statusCodeHandler = statusCodeHandler;
        }
        public async Task<IEnumerable<UserInfoDto>> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = await repository.SearchUserDetail(request.searchDetails);

            if(response.GetType() == typeof(HttpStatusCode))
            {
                statusCodeHandler.HandleStatusCode((HttpStatusCode) response);
            }

            return (IEnumerable<UserInfoDto>) response;
        }
    }
}
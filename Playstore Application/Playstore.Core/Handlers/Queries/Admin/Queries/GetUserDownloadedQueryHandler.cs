using System.Net;
using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Queries.Admin
{
    public class GetUserDownloadedQueryHandler : IRequestHandler<GetUserDownloadQuery, bool>
    {
        private readonly IAppInfoRepository repository;
        private readonly IStatusCodeHandlerRepository statusCodeHandler;
        public GetUserDownloadedQueryHandler(IAppInfoRepository repository , IStatusCodeHandlerRepository statusCodeHandler)
        {
            this.repository = repository;
            this.statusCodeHandler = statusCodeHandler;
        }
        public async Task<bool> Handle(GetUserDownloadQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await repository.GetUserDownloadedOrNot(request.UserId , request.AppId);
    
                if (response != HttpStatusCode.Found && response != HttpStatusCode.NotFound)
                {
                    this.statusCodeHandler.HandleStatusCode(response);
                }
    
                if (response == HttpStatusCode.Found)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (ApiResponseException)
            {
                
                throw;
            }
        }
    }
}
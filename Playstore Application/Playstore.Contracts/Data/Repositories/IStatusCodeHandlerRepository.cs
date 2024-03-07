using System.Net;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IStatusCodeHandlerRepository
    {
        void HandleStatusCode(HttpStatusCode statusCode);
    }
}
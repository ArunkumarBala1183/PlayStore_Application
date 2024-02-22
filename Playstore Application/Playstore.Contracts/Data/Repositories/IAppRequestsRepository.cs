namespace Playstore.Contracts.Data.Repositories
{
    public interface IAppRequestsRepository
    {
        Task<object> GetAllRequests();
    }
}
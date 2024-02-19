namespace Playstore.Contracts.Data.Repositories
{
    public interface IAppDownloadsRepository
    {
        Task<object> GetAllAppDownloads();
    }
}
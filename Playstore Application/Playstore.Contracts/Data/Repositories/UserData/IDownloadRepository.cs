namespace Playstore.Contracts.Data.Repositories
{
    public interface IDownloadRepository
    {
        Task<object> ViewAppDownloads();
    }
}
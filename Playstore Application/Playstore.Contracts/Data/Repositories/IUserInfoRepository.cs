namespace Playstore.Contracts.Data.Repositories
{
    public interface IUserInfoRepository
    {
        Task<object> ViewAllUsers();
    }
}
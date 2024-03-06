namespace Playstore.Contracts.Data.Repositories
{
    public interface IUserInfoRepository
    {
        Task<object> ViewAllUsers();

        Task<object> SearchUserDetail(string searchDetails);
    }
}
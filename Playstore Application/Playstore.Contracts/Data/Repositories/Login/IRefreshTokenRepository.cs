using Playstore.Contracts.Data.Entities;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task StoreRefreshToken(Guid userId, string refreshToken);
        Task<RefreshToken> GetRefreshToken(Guid userId);

    }
}
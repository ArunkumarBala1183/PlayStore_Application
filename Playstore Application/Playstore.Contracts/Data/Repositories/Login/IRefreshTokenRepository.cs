using Playstore.Contracts.Data.Entities;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task CommitAsync();
        Task StoreRefreshTokenAsync(Guid userId, string refreshToken);
        Task DeleteRefreshTokenAsync(Guid userId, string refreshToken);
        Task<RefreshToken> GetRefreshTokenAsync(Guid userId);

        
    }
}
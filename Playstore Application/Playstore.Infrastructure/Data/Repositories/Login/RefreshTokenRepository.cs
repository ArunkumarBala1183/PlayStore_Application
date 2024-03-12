using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Core.Exceptions;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;

namespace Playstore.Core.Data.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly DatabaseContext _context;
        public RefreshTokenRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task StoreRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var refreshTokenEntity = await _context.RefreshTokens.FirstOrDefaultAsync(id => id.UserId == userId);

            if (refreshTokenEntity == null)
            {
                refreshTokenEntity = new RefreshToken
                {
                    UserId = userId,
                    RefreshKey = refreshToken
                };
                await _context.RefreshTokens.AddAsync(refreshTokenEntity);
            }
            else
            {
                refreshTokenEntity.RefreshKey = refreshToken;
                _context.RefreshTokens.Update(refreshTokenEntity);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var refreshTokenEntity = await _context.RefreshTokens.FirstOrDefaultAsync(id => id.UserId == userId && id.RefreshKey == refreshToken);

            if (refreshTokenEntity != null)
            {
                _context.RefreshTokens.Remove(refreshTokenEntity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(Guid userId)
        {
            var userid = await _context.RefreshTokens.FirstOrDefaultAsync(id => id.UserId == userId);
            if (userid == null)
            {
                throw new EntityNotFoundException($"User with id {userid} not found.");
            }
            return userid;
        }
    }
}
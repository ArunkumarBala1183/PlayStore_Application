using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Infrastructure.Data.Repositories.Generic;
using Playstore.Migrations;
using Serilog;

namespace Playstore.Core.Data.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly DatabaseContext _context;
        private readonly ILogger logger;
        public RefreshTokenRepository(DatabaseContext context , IHttpContextAccessor httpContext) : base(context)
        {
            _context = context;
            logger = Log//.ForContext("userId", httpContext.HttpContext?.Items["userId"])
                        .ForContext("Location", typeof(RefreshTokenRepository).Name);
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
            logger.Information("Refresh Token saved to server");
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(Guid userId)
        {
            var response = await _context.RefreshTokens.FirstOrDefaultAsync(id => id.UserId == userId);
            logger.Information("Refresh Token Fetched");
            return response;
        }
    }
}
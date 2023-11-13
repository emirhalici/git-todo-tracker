using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using git_todo_tracker.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace git_todo_tracker.Repositories.RefreshToken
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private DataContext context;

        public RefreshTokenRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<Models.RefreshToken> Create(User user)
        {
            var refreshToken = new Models.RefreshToken
            {
                UserId = user.Id,
                Token = Guid.NewGuid().ToString(),
                ExpiresAt = DateTime.UtcNow.AddDays(7),
            };

            context.RefreshTokens.Add(refreshToken);
            await context.SaveChangesAsync();
            return refreshToken;
        }

        public async Task<Models.RefreshToken?> GetByToken(string refreshToken)
        {
            return await context.RefreshTokens.FindAsync(refreshToken);
        }
    }
}
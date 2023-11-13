using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace git_todo_tracker.Repositories.RefreshToken
{
    public interface IRefreshTokenRepository
    {
        public Task<Models.RefreshToken> Create(User user);
        public Task<Models.RefreshToken?> GetByToken(string RefreshToken);
    }
}
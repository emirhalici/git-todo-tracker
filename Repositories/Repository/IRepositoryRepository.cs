using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace git_todo_tracker.Repositories.Repository
{
    public interface IRepositoryRepository
    {
        public Task<GitRepository> Register(RegisterGitRepositoryRequest registerGitRepositoryRequest);
        public Task<bool> RemoveById(string repoId);
        public Task<IEnumerable<GitRepository>> GetAll();
        public Task<GitRepository?> GetById(string repoId);
    }
}
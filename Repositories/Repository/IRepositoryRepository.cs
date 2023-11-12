using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace git_todo_tracker.Repositories.Repository
{
    public interface IRepositoryRepository
    {
        public GitRepository Register(RegisterGitRepositoryRequest registerGitRepositoryRequest);
        public bool RemoveById(string repoId);
        public IEnumerable<GitRepository> GetAll();
        public GitRepository? GetById(string repoId);
    }
}
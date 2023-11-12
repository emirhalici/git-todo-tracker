using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace git_todo_tracker.Repositories.Repository
{
    public class RepositoryRepository : IRepositoryRepository
    {
        private readonly ILogger<RepositoryRepository> _logger;

        public RepositoryRepository(ILogger<RepositoryRepository> logger)
        {
            _logger = logger;
        }

        private List<GitRepository> repositories = new() {
            new GitRepository() {
                Id = "0",
                UserName = "emirhalici",
                ProjectName = "git-todo-tracker"
            }
        };

        public async Task<IEnumerable<GitRepository>> GetAll()
        {
            return repositories;
        }

        public async Task<GitRepository?> GetById(string repoId)
        {
            return repositories.SingleOrDefault(r => r?.Id == repoId, null);
        }

        public async Task<GitRepository> Register(RegisterGitRepositoryRequest registerGitRepositoryRequest)
        {
            var id = $"git-repository-{repositories.Count}";
            var newRepository = GitRepository.FromRegisterRequest(id, registerGitRepositoryRequest);
            repositories.Add(newRepository);
            return newRepository;
        }

        public async Task<bool> RemoveById(string repoId)
        {
            var repoToBeRemoved = repositories.SingleOrDefault(r => r?.Id == repoId, null);
            return repoToBeRemoved is not null && repositories.Remove(repoToBeRemoved);
        }
    }
}
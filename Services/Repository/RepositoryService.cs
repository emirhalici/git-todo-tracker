using git_todo_tracker.Repositories.Repository;
using git_todo_tracker.Services.Repository;

namespace git_todo_tracker.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly IRepositoryRepository repository;

        public RepositoryService(IRepositoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<GitRepository>> GetAllRepositories()
        {
            return await repository.GetAll();
        }

        public async Task<GitRepository> RegisterGitRepository(RegisterGitRepositoryRequest registerGitRepositoryRequest)
        {
            return await repository.Register(registerGitRepositoryRequest);
        }

        public async Task<bool> RemoveGitRepository(string repoId)
        {
            return await repository.RemoveById(repoId);
        }
    }
}
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

        public IEnumerable<GitRepository> GetAllRepositories()
        {
            return repository.GetAll();
        }

        public GitRepository RegisterGitRepository(RegisterGitRepositoryRequest registerGitRepositoryRequest)
        {
            return repository.Register(registerGitRepositoryRequest);
        }

        public bool RemoveGitRepository(string repoId)
        {
            return repository.RemoveById(repoId);
        }
    }
}
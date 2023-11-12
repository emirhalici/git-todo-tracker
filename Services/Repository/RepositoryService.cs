using git_todo_tracker.Services.Repository;

namespace git_todo_tracker.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly List<GitRepository> repositories = new() { };

        public IEnumerable<GitRepository> GetAllRepositories()
        {
            return repositories;
        }

        public GitRepository RegisterGitRepository(RegisterGitRepositoryRequest registerGitRepositoryRequest)
        {
            var id = $"git-repository-{repositories.Count}";
            var newRepository = GitRepository.FromRegisterRequest(id, registerGitRepositoryRequest);
            repositories.Add(newRepository);
            return newRepository;
        }

        public bool RemoveGitRepository(string repoId)
        {
            var repoToBeRemoved = repositories.SingleOrDefault(r => r.Id == repoId);
            return repoToBeRemoved is not null && repositories.Remove(repoToBeRemoved);
        }
    }
}
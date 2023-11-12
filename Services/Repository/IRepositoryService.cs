namespace git_todo_tracker.Services.Repository
{
    public interface IRepositoryService
    {
        public IEnumerable<GitRepository> GetAllRepositories();
        public GitRepository RegisterGitRepository(RegisterGitRepositoryRequest registerGitRepositoryRequest);
        public bool RemoveGitRepository(string repoId);
    }
}
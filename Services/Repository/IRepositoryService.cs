namespace git_todo_tracker.Services.Repository
{
    public interface IRepositoryService
    {
        public Task<IEnumerable<GitRepository>> GetAllRepositories();
        public Task<GitRepository> RegisterGitRepository(RegisterGitRepositoryRequest registerGitRepositoryRequest);
        public Task<bool> RemoveGitRepository(string repoId);
    }
}
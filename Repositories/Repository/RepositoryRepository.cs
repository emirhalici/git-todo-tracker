using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using git_todo_tracker.Data;

namespace git_todo_tracker.Repositories.Repository
{
    public class RepositoryRepository : IRepositoryRepository
    {
        private readonly ILogger<RepositoryRepository> logger;
        private readonly DataContext context;

        public RepositoryRepository(ILogger<RepositoryRepository> logger, DataContext context)
        {
            this.logger = logger;
            this.context = context;
        }


        public async Task<IEnumerable<GitRepository>> GetAll()
        {
            return await context.GitRepositories.ToListAsync();
        }

        public async Task<GitRepository?> GetById(string repoId)
        {
            return await context.GitRepositories.FindAsync(repoId);
        }

        public async Task<GitRepository> Register(RegisterGitRepositoryRequest registerGitRepositoryRequest)
        {
            var id = Guid.NewGuid().ToString();
            var newRepository = GitRepository.FromRegisterRequest(id, registerGitRepositoryRequest);
            context.GitRepositories.Add(newRepository);
            await context.SaveChangesAsync();
            return newRepository;
        }

        public async Task<bool> RemoveById(string repoId)
        {
            var repoToBeRemoved = await context.GitRepositories.FindAsync(repoId);
            if (repoToBeRemoved is null)
            {
                return false;
            }
            else
            {
                context.GitRepositories.Remove(repoToBeRemoved);
                await context.SaveChangesAsync();
                return true;
            }

        }
    }
}
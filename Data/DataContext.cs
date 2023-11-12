using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace git_todo_tracker.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<GitRepository> GitRepositories { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
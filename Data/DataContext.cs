namespace git_todo_tracker.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<GitRepository> GitRepositories { get; set; }
    }
}
namespace git_todo_tracker.Data
{
    public class DataContext : DbContext
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

        public DbSet<User> Users { get; set; }
    }
}
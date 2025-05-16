namespace Auth.API.Data
{
    public class AuthDbContext(DbContextOptions<AuthDbContext> options)
    : DbContext(options)
    { 
        public DbSet<User> Users => Set<User>();
        public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
        public DbSet<UserLogin> UserLogins => Set<UserLogin>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
        }
    }
}

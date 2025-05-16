using Auth.API.Repositories.Interfaces;

namespace Auth.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        public IUserRepository Users { get; }

        public UnitOfWork(
            AuthDbContext context, 
            IUserRepository userRepository)
        {
            _context = context;
            Users = userRepository;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

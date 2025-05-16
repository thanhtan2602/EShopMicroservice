using Auth.API.Repositories.Interfaces;

namespace Auth.API.Repositories
{
    public class UserRepository(AuthDbContext context)
        : GenericRepository<User>(context), IUserRepository
    {
        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task<User?> FindUserAsync(string userName, CancellationToken cancellationToken = default)
            => await context.Users
                    .Include(x => x.UserLogins)
                    .Where(x => x.Email == userName || x.PhoneNumber == userName)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}

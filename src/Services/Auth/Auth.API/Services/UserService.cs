using Auth.API.Repositories.Interfaces;

namespace Auth.API.Services
{
    public class UserService(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
        : IUserService
    {
        public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await userRepository.GetByIdAsync(userId, cancellationToken);
        }

        public async Task<User?> FindUserAsync(string userName, CancellationToken cancellationToken)
        {
            return await userRepository.FindUserAsync(userName, cancellationToken);
        }

        public async Task<bool> CreateUserAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                await unitOfWork.Users.AddAsync(user);
                return await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

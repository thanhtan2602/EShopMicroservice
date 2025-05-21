using Auth.API.Repositories.Interfaces;

namespace Auth.API.Services
{
    public class UserService(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
        : IUserService
    {
        public async Task<UserModel?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(userId, cancellationToken);
            if (user is null)
            {
                return null;
            }

            return new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.UserProfile?.FirstName,
                LastName = user.UserProfile?.LastName,
                PhoneNumber = user.PhoneNumber,
                Image = user.UserProfile?.Image,
            };
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

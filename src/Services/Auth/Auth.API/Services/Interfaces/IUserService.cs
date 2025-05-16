namespace Auth.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(User user, CancellationToken cancellationToken);
        Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<User> FindUserAsync(string userName, CancellationToken cancellationToken);
    }
}

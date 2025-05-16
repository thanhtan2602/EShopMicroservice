namespace Auth.API.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<User?> FindUserAsync(string userName, CancellationToken cancellationToken = default);
    }
}

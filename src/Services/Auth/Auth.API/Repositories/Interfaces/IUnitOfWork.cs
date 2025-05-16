namespace Auth.API.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        Task<bool> SaveChangesAsync();
    }

}

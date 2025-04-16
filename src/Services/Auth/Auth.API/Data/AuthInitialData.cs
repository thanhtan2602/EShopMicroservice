using Auth.API.Models;
using Marten.Schema;

namespace Auth.API.Data
{
    public class AuthInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            PopulateUsers(session);
        }

        private async void PopulateUsers(IDocumentSession session)
        {
            if (session.Query<User>().Any())
                return;

            session.Store<User>(GetPreconfiguredUsers());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<User> GetPreconfiguredUsers() => new List<User>()
        {
            new User
            {
                Id = 1,
                Email="admin@123",
                FullName="Admin",
                Password = "admin@123",
                Status = UserStatus.Active,
                IsAdmin = true
            }
        };
    }
}

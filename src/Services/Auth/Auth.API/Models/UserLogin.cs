namespace Auth.API.Models
{
    public class UserLogin : Entity<Guid>
    {
        public Guid UserId { get; set; }
        public int LoginProvider { get; set; }
        public string? ProviderKey { get; set; }
        public string? ProviderDisplayName { get; set; }
        public User User { get; set; }
    }
}

namespace Auth.API.Models
{
    public class UserLogin : Entity<int>
    {
        public int UserId { get; set; }
        public string LoginProvider { get; set; } = default!;
        public string ProviderKey { get; set; } = default!;
        public string ProviderDisplayName { get; set; } = default!;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}

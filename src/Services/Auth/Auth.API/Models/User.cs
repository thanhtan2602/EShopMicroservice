namespace Auth.API.Models
{
    public class User : Entity<Guid>
    {
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PasswordHash { get; set; }
        public bool IsAdmin { get; set; }
        public UserStatus Status { get; set; }
        public UserProfile UserProfile { get; set; }
        public ICollection<UserLogin> UserLogins { get; set; }
    }
}

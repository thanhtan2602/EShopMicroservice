namespace Auth.API.Models
{
    public class UserProfile : Entity<int>
    {
        public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }
        public User User { get; set; } = null!;
    }
}

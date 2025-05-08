namespace Auth.API.Models
{
    public class User : Entity<int>
    {
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public UserStatus Status { get; set; } = UserStatus.Pending;
        public bool IsAdmin { get; set; } = false;
    }
}

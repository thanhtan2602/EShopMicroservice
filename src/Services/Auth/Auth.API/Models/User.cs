namespace Auth.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public UserStatus Status { get; set; } = UserStatus.Pending;
        public bool IsAdmin { get; set; } = false;
    }
}

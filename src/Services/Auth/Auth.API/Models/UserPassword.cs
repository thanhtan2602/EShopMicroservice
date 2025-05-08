namespace Auth.API.Models
{
    public class UserPassword
    {
        public int UserId { get; set; }
        public string PasswordHash { get; set; } = default!;
        public DateTime? LastModified { get; set; }
    }
}

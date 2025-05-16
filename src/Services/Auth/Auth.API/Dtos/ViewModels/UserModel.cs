namespace Auth.API.Dtos.ViewModels
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Image { get; set; } = default!;
    }
}

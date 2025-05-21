namespace Auth.API.Dtos.ViewModels
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FullName
        {
            get => string.Format("{0} {1}", FirstName, LastName);
            set { }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
    }
}

namespace Auth.API.Dtos
{
    public class RevokedToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }

}

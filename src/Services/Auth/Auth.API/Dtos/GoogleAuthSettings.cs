namespace Auth.API.Dtos
{
    public class GoogleAuthSettings
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string RedirectUri { get; set; } = string.Empty;
        public string Scope { get; set; } = string.Empty;
        public string ResponseType { get; set; } = string.Empty;
    }
}

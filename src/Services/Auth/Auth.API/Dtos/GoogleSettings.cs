namespace Auth.API.Dtos
{
    public class GoogleSettings
    {
        public GoogleSettings()
        {
            
        }
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string RedirectUri { get; set; } = string.Empty;
        public string TokenEndpoint { get; set; } = string.Empty;
        public string UserInfoEndpoint { get; set; } = string.Empty;
        public string Scopes { get; set; } = string.Empty;
    }
}

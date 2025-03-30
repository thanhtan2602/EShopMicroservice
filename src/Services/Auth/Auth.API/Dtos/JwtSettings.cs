namespace Auth.API.Dtos
{
    public class JwtSettings
    {
        public JwtSettings() { }

        public string Key { get; init; } = string.Empty;
        public string Issuer { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;
        public string AccessTokenExpiryMinutes { get; init; } = string.Empty;
        public string RefreshTokenExpiryDays { get; init; } = string.Empty;
    }
}

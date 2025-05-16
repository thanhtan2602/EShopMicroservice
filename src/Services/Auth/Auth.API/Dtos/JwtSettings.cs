namespace Auth.API.Dtos
{
    public class JwtSettings
    {
        public JwtSettings() 
        { 
        
        }
        public string Key { get; init; } = default!;
        public string Issuer { get; init; } = default!;
        public string Audience { get; init; } = default!;
        public string AccessTokenExpiryMinutes { get; init; } = default!;
        public string RefreshTokenExpiryDays { get; init; } = default!;
    }
}

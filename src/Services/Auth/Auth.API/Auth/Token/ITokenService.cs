namespace Auth.API.Auth.Token
{
    public interface ITokenService
    {
        public string GenerateAccessToken(model.User user, JwtSettings jwtSettings);
        public string GenerateRefreshToken(int userId, JwtSettings jwtSettings);
    }
}

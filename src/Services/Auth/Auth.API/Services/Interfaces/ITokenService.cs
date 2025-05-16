namespace Auth.API.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateAccessToken(User user);
        public string GenerateRefreshToken(Guid userId);
        public string GetUserIdFromExpiredToken(string expiredToken);
        public Task RevokeRefreshToken(string refreshToken);

        public static (string RefreshTokenKey, string RevokedTokenKey) GetRedisKeys(string userId, string refreshToken)
        {
            return (string.Format(RedisKeys.RefreshTokenKey, userId), string.Format(RedisKeys.RevokedTokenKey, refreshToken));
        }
    }
}

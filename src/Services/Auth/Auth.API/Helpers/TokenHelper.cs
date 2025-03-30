namespace Auth.API.Helpers
{
    public static class TokenHelper
    {
        public static (string RefreshTokenKey, string RevokedTokenKey) GetRedisKeys(string userId, string refreshToken)
        {
            return (string.Format(RedisKeys.RefreshTokenKey, userId), string.Format(RedisKeys.RevokedTokenKey, refreshToken));
        }

        public static string GetRefreshTokenKey(string userId)
        {
            return string.Format(RedisKeys.RefreshTokenKey, userId);
        }
    }
}

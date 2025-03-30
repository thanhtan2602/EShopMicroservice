namespace Auth.API.Constants
{
    public static class RedisKeys
    {
        public const string AccessTokenKey = "access_token_{0}";
        public const string RefreshTokenKey = "refresh_token_{0}";
        public const string RevokedTokenKey = "revoked_token_{0}";
    }
}

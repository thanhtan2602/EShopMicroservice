using Microsoft.Extensions.Options;

namespace Auth.API.Features.Auth.Token
{
    public record RefreshTokenQuery(string RefreshToken) : IQuery<RefreshTokenResult>;
    public record RefreshTokenResult(string NewAccessToken, string NewRefreshToken);

    public class RefreshTokenQueryHandler(
        IDocumentSession session,
        IDistributedCache cache,
        ITokenService tokenService,
        IOptions<JwtSettings> jwtSettings)
        : IQueryHandler<RefreshTokenQuery, RefreshTokenResult>
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        public async Task<RefreshTokenResult?> Handle(RefreshTokenQuery query, CancellationToken cancellationToken)
        {
            var (isValid, userId, refreshTokenKey) = await ValidateAndGetUserId(query.RefreshToken);
            if (!isValid)
            {
                return null;
            }

            var user = await session.Query<model.User>().FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null)
            {
                return null;
            }

            var newAccessToken = tokenService.GenerateAccessToken(user);
            var newRefreshToken = tokenService.GenerateRefreshToken(user.Id);

            int expiryDays = 0;
            int.TryParse(_jwtSettings.RefreshTokenExpiryDays.ToString(), out expiryDays);

            // Revoke old refresh token
            await tokenService.RevokeRefreshToken(query.RefreshToken);

            // Store new refresh token
            await cache.SetStringAsync(refreshTokenKey, newRefreshToken,
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(expiryDays) });

            return new RefreshTokenResult(newAccessToken, newRefreshToken);
        }

        #region Private functions
        private async Task<(bool IsValid, int UserId, string RefreshTokenKey)> ValidateAndGetUserId(string refreshToken)
        {
            var userIdString = tokenService.GetUserIdFromExpiredToken(refreshToken);
            if (!int.TryParse(userIdString, out int userId))
            {
                return (false, 0, string.Empty);
            } 
                

            var (refreshTokenKey, revokedTokenKey) = TokenHelper.GetRedisKeys(userIdString, refreshToken);

            var storedRefreshToken = await cache.GetStringAsync(refreshTokenKey);

            if (storedRefreshToken != refreshToken)
            {
                return (false, 0, string.Empty);
            } 
                

            var isRevoked = await cache.GetStringAsync(revokedTokenKey);

            return string.IsNullOrEmpty(isRevoked) ? (true, userId, refreshTokenKey) : (false, 0, string.Empty);
        }
        #endregion
    }
}

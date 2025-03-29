using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Auth.API.Auth.Token
{
    public record RefreshTokenQuery(string RefreshToken) : IQuery<RefreshTokenResult>;
    public record RefreshTokenResult(string NewAccessToken, string NewRefreshToken);
    public class RefreshTokenHandler(
        IDocumentSession session,
        IOptions<JwtSettings> jwtSettings,
        IDistributedCache cache,
        ITokenService tokenService)
              : IQueryHandler<RefreshTokenQuery, RefreshTokenResult>
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;
        private readonly ITokenService _tokenService = tokenService;
        public async Task<RefreshTokenResult> Handle(RefreshTokenQuery query, CancellationToken cancellationToken)
        {
            var userId = GetUserIdFromExpiredToken(query.RefreshToken);
            if (string.IsNullOrEmpty(userId)) return null;

            var storedRefreshToken = await cache.GetStringAsync($"refresh_token_{userId}");
            if (storedRefreshToken != query.RefreshToken) return null;

            var isRevoked = await cache.GetStringAsync($"revoked_token_{query.RefreshToken}");
            if (!string.IsNullOrEmpty(isRevoked)) return null;

            int id = int.TryParse(userId, out id) ? id : 0;
            var user = await session.Query<model.User>().FirstOrDefaultAsync(x => x.Id == id);
            if (user is null) return null;

            var newAccessToken = _tokenService.GenerateAccessToken(user, _jwtSettings);
            var newRefreshToken = _tokenService.GenerateRefreshToken(user.Id, _jwtSettings);

            await cache.SetStringAsync($"refresh_token_{userId}", newRefreshToken,
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7) });

            return new RefreshTokenResult(newAccessToken, newRefreshToken);
        }

        private string? GetUserIdFromExpiredToken(string expiredToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false, // don't check for token expiration because the token is already expired
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try
            {
                var principal = tokenHandler.ValidateToken(expiredToken, validationParameters, out var securityToken);
                if (securityToken is not JwtSecurityToken jwtToken ||
                    !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return null;
                }

                return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

using Auth.API.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.API.Services
{
    public class TokenService(
            IOptions<AuthSettings> authSettings,
            IDistributedCache cache,
            IUserRepository userRepository)
                : ITokenService
    {
        private readonly JwtSettings _jwtSettings = authSettings.Value.Jwt;

        public string GenerateAccessToken(User user, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            if (!int.TryParse(_jwtSettings.AccessTokenExpiryMinutes.ToString(), out int expiresInMinutes))
            {
                expiresInMinutes = 60;
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expiresInMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken(Guid userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var expiryDays = 15;
            int.TryParse(_jwtSettings.RefreshTokenExpiryDays.ToString(), out expiryDays);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(expiryDays),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> RefreshAccessTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new ArgumentException("Refresh token cannot be null or empty.", nameof(refreshToken));
            }

            string userId = GetUserIdFromExpiredToken(refreshToken);
            if (string.IsNullOrEmpty(userId))
            {
                throw new SecurityTokenException("Invalid refresh token.");
            }

            var (refreshTokenKey, revokedTokenKey) = TokenHelper.GetRedisKeys(userId, refreshToken);

            var isRevoked = cache.GetStringAsync(revokedTokenKey, cancellationToken).Result;
            if (isRevoked == "revoked")
            {
                throw new SecurityTokenException("Refresh token has been revoked.");
            }

            var storedRefreshToken = cache.GetStringAsync(refreshTokenKey, cancellationToken).Result;
            if (storedRefreshToken != refreshToken)
            {
                throw new SecurityTokenException("Invalid refresh token.");
            }

            var user = await userRepository.GetByIdAsync(Guid.Parse(userId), cancellationToken);
            var roles = new List<string> { "Admin", "Customer" };
            return await Task.FromResult(GenerateAccessToken(user, roles));
        }

        public string GetUserIdFromExpiredToken(string expiredToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

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

        public async Task RevokeRefreshToken(string refreshToken)
        {
            string userId = GetUserIdFromExpiredToken(refreshToken);
            if (string.IsNullOrEmpty(userId)) return;

            var (refreshTokenKey, revokedTokenKey) = TokenHelper.GetRedisKeys(userId, refreshToken);

            // revoke refresh token
            var expiryDays = 0;
            int.TryParse(_jwtSettings.RefreshTokenExpiryDays.ToString(), out expiryDays);
            await cache.SetStringAsync(revokedTokenKey, "revoked",
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(expiryDays) });

            // remove refresh token
            await cache.RemoveAsync(refreshTokenKey);
        }
    }
}

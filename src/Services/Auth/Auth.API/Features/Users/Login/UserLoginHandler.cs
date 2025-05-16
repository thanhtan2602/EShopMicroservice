using Microsoft.Extensions.Caching.Distributed;

namespace Auth.API.Features.Users.Login
{
    public record LoginQuery(string Email, string Password) : IQuery<LoginResult>;
    public record LoginResult(UserModel User, string AccessToken, string RefreshToken);

    public class LoginQueryHandler(
        IUserService userService,
        ITokenService tokenService,
        IOptions<AuthSettings> authSettings,
        IDistributedCache cache) : IQueryHandler<LoginQuery, LoginResult>
    {
        public async Task<LoginResult?> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var user = await userService.FindUserAsync(query.Email, cancellationToken);
            try
            {
                if (user is null || !BCrypt.Net.BCrypt.Verify(query.Password, user.PasswordHash))
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            // Generate Access Token
            var accessToken = tokenService.GenerateAccessToken(user);

            // Generate and store Refresh Token
            var refreshToken = tokenService.GenerateRefreshToken(user.Id);
            var refreshTokenKey = TokenHelper.GetRefreshTokenKey(user.Id.ToString());

            var expiryDays = 0;
            int.TryParse(authSettings.Value.Jwt.RefreshTokenExpiryDays.ToString(), out expiryDays);
            await cache.SetStringAsync(refreshTokenKey, refreshToken,
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(expiryDays) });

            return new LoginResult(user.Adapt<UserModel>(), accessToken, refreshToken);
        }
    }
}

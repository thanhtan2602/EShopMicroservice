namespace Auth.API.Features.Auth.Login
{
    public record LoginQuery(string Email, string Password) : IQuery<LoginResult>;
    public record LoginResult(UserModel User, string AccessToken, string RefreshToken);

    public class LoginQueryHandler(
        IDocumentSession session,
        IOptions<JwtSettings> jwtSettings,
        ITokenService tokenService,
        IDistributedCache cache) : IQueryHandler<LoginQuery, LoginResult>
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        public async Task<LoginResult?> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var user = await session.Query<model.User>().FirstOrDefaultAsync(x => x.Email == query.Email);

            //if (user is null || !BCrypt.Net.BCrypt.Verify(query.Password, user.Password))
            //{
            //    return null;
            //}

            // Generate Access Token
            var accessToken = tokenService.GenerateAccessToken(user);

            // Generate and store Refresh Token
            var refreshToken = tokenService.GenerateRefreshToken(user.Id);
            var refreshTokenKey = TokenHelper.GetRefreshTokenKey(user.Id.ToString());

            var expiryDays = 0;
            int.TryParse(_jwtSettings.RefreshTokenExpiryDays.ToString(), out expiryDays);
            await cache.SetStringAsync(refreshTokenKey, refreshToken,
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(expiryDays) });

            return new LoginResult(user.Adapt<UserModel>(), accessToken, refreshToken);
        }
    }
}

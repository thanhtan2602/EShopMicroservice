using Microsoft.Extensions.Options;

namespace Auth.API.Auth.Login
{
    public record LoginQuery(string Email, string Password) : IQuery<LoginResult>;
    public record LoginResult(model.User User, string AccessToken, string RefreshToken);
    public class LoginQueryHandler(
        IDocumentSession session,
        IOptions<JwtSettings> jwtSettings,
        ITokenService tokenService,
        IDistributedCache cache)
              : IQueryHandler<LoginQuery, LoginResult>
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IDistributedCache _cache = cache;
        public async Task<LoginResult> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var user = await session.Query<model.User>().FirstOrDefaultAsync(x => x.Email == query.Email);

            if (user is null || !BCrypt.Net.BCrypt.Verify(query.Password, user.Password))
            {
                throw new UserNotFoundException(query.Email);
            }

            // Generate Access Token
            var accessToken = _tokenService.GenerateAccessToken(user, _jwtSettings);

            // Generate and store Refresh Token
            var refreshToken = _tokenService.GenerateRefreshToken(user.Id, _jwtSettings);
            try
            {
                await _cache.SetStringAsync($"refresh_token_{user.Id}", refreshToken,
                    new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7) });
            }
            catch (Exception ex)
            {

                throw;
            }

            return new LoginResult(user, accessToken, refreshToken);
        }
    }
}

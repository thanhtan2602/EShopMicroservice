using Auth.API.Features.Auth.Login;

namespace Auth.API.Features.Social.Google
{
    public record GoogleLoginCommand(GoogleUserInfo GoogleUser) : ICommand<GoogleLoginResult>;
    public record GoogleLoginResult(UserModel User, string AccessToken, string RefreshToken);
    public class GoogleLoginHandler(
                IDocumentSession session,
                IOptions<JwtSettings> jwtSettings,
                ITokenService tokenService,
                IDistributedCache cache)
        : ICommandHandler<GoogleLoginCommand, GoogleLoginResult>
    {
        public async Task<GoogleLoginResult> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
        {
            // Check if the user exists in the database
            var user = await session.Query<model.User>().FirstOrDefaultAsync(x => x.Email == request.GoogleUser.Email);

            if (user is null)
            {
                return null;
            }

            //// Generate Access Token
            //var accessToken = tokenService.GenerateAccessToken(user);

            //// Generate and store Refresh Token
            //var refreshToken = tokenService.GenerateRefreshToken(user.Id);
            //var refreshTokenKey = TokenHelper.GetRefreshTokenKey(user.Id.ToString());

            //var expiryDays = 0;
            //int.TryParse(_jwtSettings.RefreshTokenExpiryDays.ToString(), out expiryDays);
            //await cache.SetStringAsync(refreshTokenKey, refreshToken,
            //    new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(expiryDays) });

            return null; // new LoginResult(user.Adapt<UserModel>(), accessToken, refreshToken);
        }
    }
}

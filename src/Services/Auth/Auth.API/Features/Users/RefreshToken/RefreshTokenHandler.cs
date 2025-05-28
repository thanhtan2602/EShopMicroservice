namespace Auth.API.Features.Users.RefreshToken
{
    public record RefreshTokenQuery(string RefreshToken) : IQuery<RefreshTokenResult>;
    public record RefreshTokenResult(string AccessToken);
    public class RefreshTokenQueryHandler(
            ITokenService tokenService) 
        : IQueryHandler<RefreshTokenQuery, RefreshTokenResult>
    {
        public async Task<RefreshTokenResult> Handle(RefreshTokenQuery query, CancellationToken cancellationToken)
        {
            var accessToken = await tokenService.RefreshAccessTokenAsync(query.RefreshToken, cancellationToken);
            if (string.IsNullOrEmpty(accessToken))
            {
                return null; // or throw an exception if you prefer
            }

            return new RefreshTokenResult(accessToken);
        }
    }
}

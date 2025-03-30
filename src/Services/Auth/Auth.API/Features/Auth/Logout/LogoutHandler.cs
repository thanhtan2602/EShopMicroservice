using Auth.API.Features.Auth.Token;

namespace Auth.API.Features.Auth.Logout
{
    public record LogoutQuery(string RefreshToken) : IQuery<LogoutResult>;

    public record LogoutResult(bool IsSuccess = true);

    public class LogoutQueryHandler(ITokenService tokenService)
        : IQueryHandler<LogoutQuery, LogoutResult>
    {
        public async Task<LogoutResult> Handle(LogoutQuery query, CancellationToken cancellationToken)
        {
            await tokenService.RevokeRefreshToken(query.RefreshToken);

            return new LogoutResult();
        }
    }
}

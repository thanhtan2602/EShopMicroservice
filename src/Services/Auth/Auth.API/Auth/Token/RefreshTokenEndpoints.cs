
namespace Auth.API.Auth.Token
{
    public record RefreshTokenRequest(string RefreshToken);
    public record RefreshTokenResponse(string NewAccessToken, string NewRefreshToken);
    public class RefreshTokenEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/auth/refresh-token", async (RefreshTokenRequest request, ISender sender) =>
            {
                var query = request.Adapt<RefreshTokenQuery>();

                var result = await sender.Send(query);

                var response = result.Adapt<RefreshTokenResponse>();

                return Results.Ok(response);
            });
        }
    }
}

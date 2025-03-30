namespace Auth.API.Features.Auth.Token
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

                if (result is null)
                {
                    return Results.Unauthorized();
                }

                var response = result.Adapt<RefreshTokenResponse>();
                return Results.Ok(response);
            })
           .WithName("RefreshToken")
           .Produces<RefreshTokenResponse>(StatusCodes.Status200OK)
           .Produces(StatusCodes.Status401Unauthorized)
           .WithSummary("Refresh Token")
           .WithDescription("Refresh JWT Token by old refresh token.");
        }
    }
}

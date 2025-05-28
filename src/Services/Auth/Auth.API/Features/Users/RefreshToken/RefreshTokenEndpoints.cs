
namespace Auth.API.Features.Users.RefreshToken
{
    public class RefreshTokenEndpoints : ICarterModule
    {
        public record RefreshTokenRequest(string RefreshToken);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/auth-service/refresh-token", async (string refreshToken, ISender sender) =>
            {
                var query = new RefreshTokenQuery(refreshToken);
                var result = await sender.Send(query);

                if (result is null)
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(result);
            });
        }
    }
}

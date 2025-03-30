using Auth.API.Features.Auth.Register;

namespace Auth.API.Features.Auth.Logout
{
    public record LogoutRequest(string RefreshToken);
    public record LogoutResponse(bool IsSuccess);
    public class LogoutEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/auth/logout", async (LogoutRequest request, ISender sender) =>
            {
                var query = request.Adapt<LogoutQuery>();

                var result = await sender.Send(query);

                var response = result.Adapt<LogoutResponse>();

                return Results.Ok(response);
            })
            .WithName("Logout")
            .Produces<RegisterResponse>(StatusCodes.Status200OK)
            .WithSummary("Logout")
            .WithDescription("Logout");
        }
    }
}

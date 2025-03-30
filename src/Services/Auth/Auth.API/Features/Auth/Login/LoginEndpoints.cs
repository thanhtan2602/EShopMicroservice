using Auth.API.Features.Auth.Register;
using Auth.API.Features.Auth.Token;

namespace Auth.API.Features.Auth.Login
{
    public record LoginRequest(string Email, string Password);
    public record LoginResponse(model.User User, string AccessToken, string RefreshToken);
    public class LoginEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/auth/login", async (LoginRequest request, ISender sender) =>
            {
                var query = request.Adapt<LoginQuery>();

                var result = await sender.Send(query);

                if (result is null)
                {
                    return Results.Unauthorized();
                }

                var response = result.Adapt<LoginResponse>();

                return Results.Ok(response);
            })
            .WithName("Login")
            .Produces<RefreshTokenResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithSummary("Login")
            .WithDescription("Login");
        }
    }
}

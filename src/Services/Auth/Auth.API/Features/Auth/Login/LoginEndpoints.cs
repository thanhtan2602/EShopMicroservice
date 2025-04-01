namespace Auth.API.Features.Auth.Login
{
    public record LoginRequest(string Email, string Password);
    public record LoginResponse(UserModel User, string AccessToken, string RefreshToken);
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
                    return Results.NotFound();
                }

                var response = result.Adapt<LoginResponse>();

                return Results.Ok(response);
            })
            .WithName("Login")
            .Produces<RefreshTokenResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Login")
            .WithDescription("Login");
        }
    }
}

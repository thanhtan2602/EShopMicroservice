namespace Auth.API.Auth.Login
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

                var response = result.Adapt<LoginResponse>();

                return Results.Ok(response);
            });
        }
    }
}

namespace Auth.API.Features.Auth.Register
{
    public record RegisterRequest(string FullName, string Password, string Email);
    public record RegisterResponse(bool IsSuccess);
    public class RegisterEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/auth/register",
                async (RegisterRequest request, ISender sender) =>
                {
                    var command = request.Adapt<RegisterCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<RegisterResponse>();

                    return Results.Ok(response.IsSuccess);
                })
            .WithName("Create Account")
            .Produces<RegisterResponse>(StatusCodes.Status200OK)
            .WithSummary("Create Account")
            .WithDescription("Create Account");
        }
    }
}

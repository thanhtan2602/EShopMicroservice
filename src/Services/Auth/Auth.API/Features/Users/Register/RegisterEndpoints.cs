
namespace Auth.API.Features.Users.Register
{
    public class RegisterEndpoints : ICarterModule
    {
        public record RegisterRequest(string Email, string Password, string FirstName, string LastName);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/auth-service/register", async (RegisterRequest request, IMediator mediator) =>
            {
                var command = request.Adapt<RegisterCommand>();
                var result = await mediator.Send(command);
                return result is null ? Results.BadRequest() : Results.Ok(result);
            });
        }
    }
}

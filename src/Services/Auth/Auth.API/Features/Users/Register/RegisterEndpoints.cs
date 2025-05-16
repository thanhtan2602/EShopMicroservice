
namespace Auth.API.Features.Users.Register
{
    public class RegisterEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/auth-service/register", async (RegisterCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result is null ? Results.BadRequest() : Results.Ok(result);
            });
        }
    }
}

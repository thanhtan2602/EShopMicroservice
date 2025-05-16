namespace Auth.API.Features.Users.Profile
{
    public class UserProfileEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/auth-service/me", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetUserProfileQuery());
                return result is null ? Results.BadRequest() : Results.Ok(result);
            });
        }
    }
    {
    }
}

namespace Auth.API.User.CreateUser
{
    public record CreateUserRequest(string FullName, string Password, string Email, bool IsAdmin);
    public record CreateUserResponse(int Id);
    public class CreateUserEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/users",
                async (CreateUserRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateUserCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<CreateUserResponse>();

                    return Results.Created($"/users/{response.Id}", response);
                })
            .WithName("Create User")
            .Produces<CreateUserResponse>(StatusCodes.Status400BadRequest)
            .WithSummary("Create User")
            .WithDescription("Create User");
        }
    }
}

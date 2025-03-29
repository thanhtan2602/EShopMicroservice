namespace Auth.API.User.DeleteUser
{
    public record DeleteUserResponse(bool isSuccess);
    public class DeleteUserEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/users/{id}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteUserCommand(id));
                var response = new DeleteUserResponse(result.isSuccess);

                return Results.Ok(response);
            })
            .WithName("DeleteUser")
            .Produces<DeleteUserResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete User")
            .WithDescription("Delete User");
        }
    }
}

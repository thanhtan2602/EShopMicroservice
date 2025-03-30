namespace Auth.API.Features.User.GetUserById
{
    public record GetUserByIdResponse(model.User User);
    public class GetUserByIdEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/users/{id}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new GetUserByIdQuery(id));
                var response = result.Adapt<GetUserByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetUserById")
            .Produces<GetUserByIdResponse>(StatusCodes.Status400BadRequest)
            .WithSummary("GetUserById")
            .WithDescription("Get User by Id");
        }
    }
}

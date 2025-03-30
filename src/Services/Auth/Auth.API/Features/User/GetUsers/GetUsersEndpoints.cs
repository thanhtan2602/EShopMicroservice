namespace Auth.API.Features.User.GetUsers
{
    public record GetUsersRequest(int? PageNumber = 1, int? PageSize = 10);
    public record GetUsersResponse(IEnumerable<model.User> Users);
    public class GetUsersEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/users", async ([AsParameters] GetUsersRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetUsersQuery>();

                var result = await sender.Send(query);

                var response = result.Adapt<GetUsersResponse>();

                return Results.Ok(response);
            })
            .WithName("GetUsers")
            .Produces<GetUsersResponse>(StatusCodes.Status400BadRequest)
            .WithSummary("GetUsers")
            .WithDescription("Get Users");
        }
    }
}

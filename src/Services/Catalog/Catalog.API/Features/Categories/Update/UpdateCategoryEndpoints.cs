namespace Catalog.API.Features.Categorys.UpdateCategory
{
    public record UpdateCategoryRequest(Guid Id, string Name,string Description, string ImageFile);
    public record UpdateCategoryResponse(bool isSuccess);
    public class UpdateCategoryEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/categories", async (UpdateCategoryRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateCategoryCommand>();

                var result = await sender.Send(command);

                var response = new UpdateCategoryResponse(result.isSuccess);

                return Results.Ok(response);
            })
            .WithName("UpdateCategory")
            .Produces<UpdateCategoryResponse>(StatusCodes.Status400BadRequest)
            .WithSummary("UpdateCategory")
            .WithDescription("Create Category");
        }
    }
}

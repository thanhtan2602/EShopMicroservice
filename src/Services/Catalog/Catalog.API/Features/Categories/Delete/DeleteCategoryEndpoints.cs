namespace Catalog.API.Features.Categorys.DeleteCategory
{
    public record DeleteCategoryResponse(bool isSuccess);
    public class DeleteCategoryEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/categories/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteCategoryCommand(id));
                var response = new DeleteCategoryResponse(result.isSuccess);

                return Results.Ok(response);
            })
            .WithName("DeleteCategory")
            .Produces<DeleteCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Category")
            .WithDescription("Delete Category");
        }
    }
}

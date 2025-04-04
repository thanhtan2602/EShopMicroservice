namespace Catalog.API.Features.Categorys.CreateCategory
{
    public record CreateCategoryRequest(string Name, string Description, string ImageFile);
    public record CreateCategoryResponse(Guid Id);
    public class CreateCategoryEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/categories",
                async (CreateCategoryRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateCategoryCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateCategoryResponse>();

                return Results.Created($"/categories/{response.Id}", response);
            })
            .WithName("CreateCategory")
            .Produces<CreateCategoryResponse>(StatusCodes.Status400BadRequest)
            .WithSummary("CreateCategory")
            .WithDescription("Create Category");
        }
    }
}

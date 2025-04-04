
namespace Catalog.API.Features.Products.GetProductByCategory
{
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);

    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}",
                async (Guid CategoryId, ISender sender) =>
                {
                    var result = await sender.Send(new GetProductByCategoryQuery(CategoryId));

                    var response = result.Adapt<GetProductByCategoryResponse>();

                    return Results.Ok(response);
                })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Category")
            .WithDescription("Get Product By Category");
        }
    }
}

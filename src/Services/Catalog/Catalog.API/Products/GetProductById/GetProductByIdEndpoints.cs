
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductById
{
  public record GetProductByIdResponse(Product Product);
  public class GetProductByIdEndpoints : ICarterModule
  {
    public void AddRoutes(IEndpointRouteBuilder app)
    {
      app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
      {
        var result = await sender.Send(new GetProductByIdQuery(id));
        var response = result.Adapt<GetProductByIdResponse>();

        return Results.Ok(response);
      })
      .WithName("GetProductById")
      .Produces<GetProductsResponse>(StatusCodes.Status400BadRequest)
      .WithSummary("GetProductById")
      .WithDescription("Get Product by Id");
    }
  }
}

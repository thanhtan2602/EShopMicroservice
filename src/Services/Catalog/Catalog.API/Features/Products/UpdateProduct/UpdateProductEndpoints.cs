﻿namespace Catalog.API.Features.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, Guid CategoryId, string Description, string ImageFile, decimal Price);
    public record UpdateProductResponse(bool isSuccess);
    public class UpdateProductEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();

                var result = await sender.Send(command);

                var response = new UpdateProductResponse(result.isSuccess);

                return Results.Ok(response);
            })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status400BadRequest)
            .WithSummary("UpdateProduct")
            .WithDescription("Create Product");
        }
    }
}

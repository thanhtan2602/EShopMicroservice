
namespace Catalog.API.Features.Categorys.GetCategoryById
{
    public record GetCategoryByIdResponse(Category Category);
    public class GetCategoryByIdEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/categories/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetCategoryByIdQuery(id));
                var response = result.Adapt<GetCategoryByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetCategoryById")
            .Produces<GetCategoryByIdResponse>(StatusCodes.Status400BadRequest)
            .WithSummary("GetCategoryById")
            .WithDescription("Get Category by Id");
        }
    }
}

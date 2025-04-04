namespace Catalog.API.Features.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(Guid CategoryId) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    internal class GetProductByCategoryQueryHandler
        (IDocumentSession session)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                .Where(p => p.CategoryId == query.CategoryId)
                .ToListAsync(cancellationToken);

            return new GetProductByCategoryResult(products);
        }
    }
}

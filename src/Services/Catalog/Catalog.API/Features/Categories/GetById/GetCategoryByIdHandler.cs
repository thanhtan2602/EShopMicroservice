namespace Catalog.API.Features.Categorys.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid Id) : IQuery<GetCategoryByIdResult>;
    public record GetCategoryByIdResult(Category Category);
    internal class GetCategoryByIdQueryHandler(IDocumentSession session)
      : IQueryHandler<GetCategoryByIdQuery, GetCategoryByIdResult>
    {
        public async Task<GetCategoryByIdResult> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Category>(query.Id, cancellationToken);

            if (product is null)
            {
                throw new NotFoundException("Category", query.Id);
            }

            return new GetCategoryByIdResult(product);
        }
    }
}

namespace Catalog.API.Features.Categories.GetCategories
{
    public record GetCategoriesQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetCategoriesResult>;
    public record GetCategoriesResult(IEnumerable<Category> Categories, int TotalCount);
    internal class GetCategoriesHandler(IDocumentSession session)
      : IQueryHandler<GetCategoriesQuery, GetCategoriesResult>
    {
        public async Task<GetCategoriesResult> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await session.Query<Category>()
                .ToPagedListAsync(request.PageNumber ?? 1, request.PageSize ?? 10, cancellationToken);

            var totalCount = await session.Query<Category>().CountAsync();

            return new GetCategoriesResult(categories, totalCount);
        }
    }
}

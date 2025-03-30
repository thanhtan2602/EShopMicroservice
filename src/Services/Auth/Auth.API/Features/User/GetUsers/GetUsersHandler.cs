using Marten.Pagination;

namespace Auth.API.Features.User.GetUsers
{
    public record GetUsersQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetUsersResult>;
    public record GetUsersResult(IEnumerable<model.User> Users);
    internal class GetUsersHandler(IDocumentSession session)
      : IQueryHandler<GetUsersQuery, GetUsersResult>
    {
        public async Task<GetUsersResult> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await session.Query<model.User>().ToPagedListAsync(request.PageNumber ?? 1, request.PageSize ?? 10, cancellationToken);

            return new GetUsersResult(users);
        }
    }
}

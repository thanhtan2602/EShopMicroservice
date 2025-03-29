namespace Auth.API.User.GetUserById
{
    public record GetUserByIdQuery(int Id) : IQuery<GetUserByIdResult>;
    public record GetUserByIdResult(model.User User);
    internal class GetUserByIdQueryHandler(IDocumentSession session)
      : IQueryHandler<GetUserByIdQuery, GetUserByIdResult>
    {
        public async Task<GetUserByIdResult> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var user = await session.LoadAsync<model.User>(query.Id, cancellationToken);

            if (user is null)
            {
                throw new UserNotFoundException(query.Id);
            }

            return new GetUserByIdResult(user);
        }
    }
}

namespace Auth.API.Features.User.DeleteUser
{
    public record DeleteUserCommand(int Id)
      : ICommand<DeleteUserResult>;
    public record DeleteUserResult(bool isSuccess);
    internal class DeleteUserCommandHandler(IDocumentSession session)
      : ICommandHandler<DeleteUserCommand, DeleteUserResult>
    {
        public async Task<DeleteUserResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            session.Delete<model.User>(command.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteUserResult(true);
        }
    }
}

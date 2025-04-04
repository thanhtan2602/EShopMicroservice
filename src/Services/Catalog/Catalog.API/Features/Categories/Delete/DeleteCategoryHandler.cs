namespace Catalog.API.Features.Categorys.DeleteCategory
{
    public record DeleteCategoryCommand(Guid Id)
      : ICommand<DeleteCategoryResult>;
    public record DeleteCategoryResult(bool isSuccess);
    internal class DeleteCategoryCommandHandler(IDocumentSession session)
      : ICommandHandler<DeleteCategoryCommand, DeleteCategoryResult>
    {
        public async Task<DeleteCategoryResult> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            session.Delete<Category>(command.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteCategoryResult(true);
        }
    }
}

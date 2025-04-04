namespace Catalog.API.Features.Categorys.CreateCategory
{
    public record CreateCategoryCommand(string Name, string Description, string ImageFile)
        : ICommand<CreateCategoryResult>;
    public record CreateCategoryResult(Guid Id);

    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }

    internal class CreateCategoryCommandHandler(IDocumentSession session)
        : ICommandHandler<CreateCategoryCommand, CreateCategoryResult>
    {
        public async Task<CreateCategoryResult> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = command.Name,
                Description = command.Description,
                ImageFile = command.ImageFile,
            };

            session.Store(category);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateCategoryResult(category.Id);
        }
    }
}

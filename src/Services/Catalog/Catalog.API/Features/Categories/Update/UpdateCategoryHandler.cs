using BuildingBlocks.Exceptions;

namespace Catalog.API.Features.Categorys.UpdateCategory
{
    public record UpdateCategoryCommand(Guid Id, string Name, string Description, string ImageFile)
      : ICommand<UpdateCategoryResult>;
    public record UpdateCategoryResult(bool isSuccess);

    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("CategoryId is required");
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name is required");
        }
    }

    internal class UpdateCategoryCommandHandler(IDocumentSession session)
      : ICommandHandler<UpdateCategoryCommand, UpdateCategoryResult>
    {
        public async Task<UpdateCategoryResult> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await session.LoadAsync<Category>(command.Id, cancellationToken);
            if (category is null)
            {
                throw new NotFoundException("Category", command.Id);
            }

            category.Name = command.Name;
            category.Description = command.Description;
            category.ImageFile = command.ImageFile;

            session.Update(category);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateCategoryResult(true);
        }
    }
}

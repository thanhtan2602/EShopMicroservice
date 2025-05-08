namespace Auth.API.Features.User.CreateUser
{
    public record CreateUserCommand(string FullName, string Password, string Email, bool IsAdmin)
        : ICommand<CreateUserResult>;
    public record CreateUserResult(int Id);

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("User name is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }

    internal class CreateUserCommandHandler(IDocumentSession session)
        : ICommandHandler<CreateUserCommand, CreateUserResult>
    {
        public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = new model.User
            {
                FullName = command.FullName,
                //Password = BCrypt.Net.BCrypt.HashPassword(command.Password),
                Email = command.Email,
                Status = UserStatus.Pending,
                IsAdmin = command.IsAdmin
            };


            session.Store(user);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateUserResult(user.Id);
        }
    }
}

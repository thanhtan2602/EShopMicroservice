using userModel = Auth.API.Models;

namespace Auth.API.User.CreateUser
{
    public record CreateUserCommand(string Username, string Password, string Email, bool IsAdmin)
        : ICommand<CreateUserResult>;
    public record CreateUserResult(int Id);

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("User name is required");
        }
    }

    internal class CreateUserCommandHandler(IDocumentSession session)
        : ICommandHandler<CreateUserCommand, CreateUserResult>
    {
        public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = new userModel.User
            {
                Username = command.Username,
                Password = command.Password,
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

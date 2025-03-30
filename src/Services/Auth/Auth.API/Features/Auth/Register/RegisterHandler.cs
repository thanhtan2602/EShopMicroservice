namespace Auth.API.Features.Auth.Register
{
    public record RegisterCommand(string FullName, string Password, string Email)
            : ICommand<RegisterResult>;
    public record RegisterResult(bool IsSuccess = true);

    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("User name is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }

    internal class RegisterCommandHandler(IDocumentSession session)
        : ICommandHandler<RegisterCommand, RegisterResult>
    {
        public async Task<RegisterResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var user = new model.User
            {
                FullName = command.FullName,
                Password = BCrypt.Net.BCrypt.HashPassword(command.Password),
                Email = command.Email,
                Status = UserStatus.Pending,
                IsAdmin = false
            };


            session.Store(user);
            await session.SaveChangesAsync(cancellationToken);

            return new RegisterResult();
        }
    }
}

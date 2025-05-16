namespace Auth.API.Features.Users.Register
{
    public record RegisterResult(bool IsSuccess);
    public record RegisterCommand(string Email, string Password, string FirstName, string LastName) 
        : ICommand<RegisterResult>;
    //public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    //{
    //    public RegisterCommandValidator()
    //    {
    //        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
    //        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
    //    }
    //}
    public class RegisterCommandHandler(
        IUserService userService) 
        : ICommandHandler<RegisterCommand, RegisterResult>
    {
        public async Task<RegisterResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var isExists = await userService.FindUserAsync(command.Email, cancellationToken);
            if (isExists != null)
            {
                return new RegisterResult(false);
            }

            var user = new User
            {
                Email = command.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password),
                Status = UserStatus.Active,
                UserProfile = new UserProfile
                {
                    FirstName = command.FirstName,
                    LastName = command.LastName
                },
                UserLogins = new List<UserLogin>
                {
                    new UserLogin
                    {
                        LoginProvider = (int)UserLoginProvider.SystemAccount,
                        ProviderKey = command.Email,
                        ProviderDisplayName = UserLoginProvider.SystemAccount.GetDescription(),
                        CreatedAt = DateTime.UtcNow
                    }
                },
            };

            await userService.CreateUserAsync(user, cancellationToken);

            return new RegisterResult(true);
        }
    }
}

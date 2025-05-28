
using Auth.API.Services;
using Microsoft.Extensions.Logging;

namespace Auth.API.Features.Google
{
    public record GoogleLoginCommand(GoogleUserInfoResponse GoogleUser) : ICommand<GoogleLoginResult>;
    public record GoogleLoginResult(UserModel User, string AccessToken, string RefreshToken);
    public class GoogleSigninHandler(
        IUserService userService,
        ITokenService tokenService,
        ILogger<GoogleSigninHandler> logger)
        : ICommandHandler<GoogleLoginCommand, GoogleLoginResult>
    {
        public async Task<GoogleLoginResult> Handle(GoogleLoginCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling Google login for user: {Email}", command.GoogleUser.Email);

            var googleUser = command.GoogleUser;

            var user = await userService.FindUserAsync(googleUser.Email, cancellationToken);

            if (user is null)
            {
                user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = googleUser.Email,
                    CreatedAt = DateTime.UtcNow,
                    Status = UserStatus.Active,
                    UserProfile = new UserProfile
                    {
                        FirstName = googleUser.GivenName,
                        LastName = googleUser.FamilyName,
                        Image = googleUser.Picture
                    },
                    UserLogins = new List<UserLogin>
                    {
                        new UserLogin
                        {
                            LoginProvider = (int)UserLoginProvider.Google,
                            ProviderKey = googleUser.Id,
                            ProviderDisplayName = UserLoginProvider.Google.GetDescription(),
                            CreatedAt = DateTime.UtcNow
                        }
                    }
                };

                await userService.CreateUserAsync(user, cancellationToken);
            }
            else
            {
                // Ensure Google login is associated
                if (!user.UserLogins.Any(ul =>
                        ul.LoginProvider == (int)UserLoginProvider.Google &&
                        ul.ProviderKey == googleUser.Id))
                {
                    user.UserLogins.Add(new UserLogin
                    {
                        LoginProvider = (int)UserLoginProvider.Google,
                        ProviderKey = googleUser.Id,
                        ProviderDisplayName = UserLoginProvider.Google.GetDescription(),
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            // Generate tokens
            var accessToken = tokenService.GenerateAccessToken(user, new List<string> { "Admin", "Customer" });
            var refreshToken = tokenService.GenerateRefreshToken(user.Id);

            var userModel = new UserModel
            {
                Id = user.Id, 
                Email = user.Email,
                Image = user.UserProfile?.Image
            };

            return new GoogleLoginResult(userModel, accessToken, refreshToken);
        }
    }
}

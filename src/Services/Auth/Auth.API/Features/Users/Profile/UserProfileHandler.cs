using System.Security.Claims;

namespace Auth.API.Features.Users.Profile
{
    public record UserProfileResult(UserModel User);
    public record GetUserProfileQuery() : IRequest<UserProfileResult>;
    public class UserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileResult>
    {
        public Task<UserProfileResult> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            
        }
    }
}

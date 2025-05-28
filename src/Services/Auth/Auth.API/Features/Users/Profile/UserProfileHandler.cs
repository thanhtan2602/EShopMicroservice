using BuildingBlocks.Exceptions;

namespace Auth.API.Features.Users.Profile
{
    public record UserProfileResult(UserModel User);
    public record GetUserProfileQuery(Guid UserId) : IQuery<UserProfileResult>;
    public class UserProfileQueryHandler(
            IUserService userService
        ) : IQueryHandler<GetUserProfileQuery, UserProfileResult>
    {
        public async Task<UserProfileResult> Handle(GetUserProfileQuery query, CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByIdAsync(query.UserId, cancellationToken);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }

            return new UserProfileResult(user);
        }
    }
}

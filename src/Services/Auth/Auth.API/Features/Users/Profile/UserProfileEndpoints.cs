using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Auth.API.Features.Users.Profile
{
    public class UserProfileEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/auth-service/me", [Authorize] async (HttpContext httpContext, IMediator mediator) =>
            {
                var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Results.Unauthorized();
                }
                var result = await mediator.Send(new GetUserProfileQuery(new Guid(userId)));
                return result is null ? Results.BadRequest() : Results.Ok(result);
            });
        }
    }
}

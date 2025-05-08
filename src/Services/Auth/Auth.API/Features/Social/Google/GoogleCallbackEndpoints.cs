namespace Auth.API.Features.Social.Google
{
    public class GoogleCallbackEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/auth-service/google/callback", async (HttpContext context, IConfiguration config, IMediator mediator, string code) =>
            {
                var tokenResponse = await GoogleSignInHelpper.GetGoogleTokenAsync(code, config);
                if (tokenResponse is null)
                    return Results.BadRequest("Cannot get token from Google");

                var userInfo = await GoogleSignInHelpper.GetGoogleUserInfoAsync(tokenResponse.AccessToken);
                if (userInfo is null)
                    return Results.BadRequest("Cannot get user info");

                var result = await mediator.Send(new GoogleLoginCommand(userInfo));
                //return Results.Redirect($"https://your-frontend.com/oauth-success?token={result.Token}");
                return Results.Ok(result);
            });
        }
    }
}

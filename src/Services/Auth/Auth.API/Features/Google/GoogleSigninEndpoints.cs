namespace Auth.API.Features.Google
{
    public class GoogleSigninEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // This endpoint is used to initiate the Google sign-in process
            app.MapGet("/auth-service/google/signin", (IConfiguration config) =>
            {
                var clientId = config["Authentication:Google:ClientId"]!;
                var redirectUri = config["Authentication:Google:RedirectUri"]!;
                var scope = config["Authentication:Google:Scope"]!;
                var responseType = config["Authentication:Google:ResponseType"]!;

                var url = $"https://accounts.google.com/o/oauth2/v2/auth?" +
                          $"client_id={clientId}&redirect_uri={redirectUri}" +
                          $"&response_type={responseType}&scope={scope}";

                return Results.Redirect(url);
            })
            .WithName("GoogleSignin");

            // This endpoint is used as a callback URL after Google sign-in
            app.MapGet("/auth-service/google/callback", async (HttpContext context, IConfiguration config, IMediator mediator, string code) =>
            {
                var tokenResponse = await GoogleAuthHelper.GetGoogleTokenAsync(code, config);
                if (tokenResponse is null)
                    return Results.BadRequest("Cannot get token from Google");

                var userInfo = await GoogleAuthHelper.GetGoogleUserInfoAsync(tokenResponse.AccessToken);
                if (userInfo is null)
                    return Results.BadRequest("Cannot get user info");

                var result = await mediator.Send(new GoogleLoginCommand(userInfo));
                var tokenResult = new
                {
                    accessToken = result.AccessToken,
                    refreshToken = result.RefreshToken,
                    user = result.User
                };

                var json = JsonSerializer.Serialize(tokenResult);

                var html = $@"
                    <html>
                      <body>
                        <script>
                          window.opener.postMessage({json}, 'http://localhost:3001');
                          window.close();
                        </script>
                      </body>
                    </html>";

                return Results.Content(html, "text/html");
            });
        }
    }
}

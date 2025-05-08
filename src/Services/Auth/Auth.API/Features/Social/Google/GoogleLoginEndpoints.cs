namespace Auth.API.Features.Social.Google
{
    public class GoogleLoginEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/auth-service/google/login", (IConfiguration config) =>
            {
                var clientId = config["Authentication:Google:ClientId"]!;
                var redirectUri = config["Authentication:Google:RedirectUri"]!;
                var scope = config["Authentication:Google:Scope"]!;
                var responseType = config["Authentication:Google:ResponseType"]!;

                var url = $"https://accounts.google.com/o/oauth2/v2/auth?" +
                          $"client_id={clientId}&redirect_uri={redirectUri}" +
                          $"&response_type={responseType}&scope={scope}";

                return Results.Redirect(url);
            });
        }
    }
}

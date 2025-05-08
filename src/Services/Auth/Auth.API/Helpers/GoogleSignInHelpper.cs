using System.Net.Http.Headers;
using System.Text.Json;

namespace Auth.API.Helpers
{
    public static class GoogleSignInHelpper
    {
        public static async Task<GoogleTokenResponse?> GetGoogleTokenAsync(string code, IConfiguration config)
        {
            using var client = new HttpClient();

            var values = new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", config["Authentication:Google:ClientId"]! },
                { "client_secret", config["Authentication:Google:ClientSecret"]! },
                { "redirect_uri", "https://localhost:5056/auth-service/google/callback" },
                { "grant_type", "authorization_code" }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://oauth2.googleapis.com/token", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GoogleTokenResponse>(json);
        }

        public static async Task<GoogleUserInfo?> GetGoogleUserInfoAsync(string accessToken)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync("https://www.googleapis.com/oauth2/v2/userinfo");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GoogleUserInfo>(json);
        }

    }
}

namespace Auth.API.Middlewares
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;

            await _next(context);

            //if (path.StartsWithSegments("/public"))
            //{
            //    await _next(context);
            //    return;
            //}

            if (!context.Request.Headers.TryGetValue("Authorization", out var tokenHeader))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Missing Authorization Header");
                return;
            }

            var token = tokenHeader.ToString().Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);

            if (string.IsNullOrWhiteSpace(token) || token != "your-valid-token")
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid Token");
                return;
            }

            await _next(context);
        }
    }

}

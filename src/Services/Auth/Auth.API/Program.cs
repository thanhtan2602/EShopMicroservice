var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    var assembly = typeof(Program).Assembly;

    services.AddCarter();
    services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
    services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
    services.AddDistributedMemoryCache();

    services.AddMarten(opts =>
        opts.Connection(configuration.GetConnectionString("Database")!)
    ).UseLightweightSessions();

    services.AddScoped<ITokenService, TokenService>();

    // Authentication
    var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero
            };
        });

    services.AddAuthorization();

    // CORS Configuration
    services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend", policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
    });
}

void ConfigureMiddleware(WebApplication app)
{
    app.UseCors("AllowFrontend");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapCarter();
}
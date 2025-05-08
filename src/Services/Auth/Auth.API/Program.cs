using Auth.API.Data;
using Auth.API.Models;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services, builder.Configuration, builder.Environment);

var app = builder.Build();

// Configure the HTTP request pipeline.
ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
{
    var assembly = typeof(Program).Assembly;

    services.AddCarter();
    services.AddMediatR(config =>
    {
        config.RegisterServicesFromAssembly(assembly);
        config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    });

    // Validators
    services.AddValidatorsFromAssembly(assembly);

    services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
    services.Configure<GoogleAuthSettings>(configuration.GetSection("Authentication:Google"));
    services.AddDistributedMemoryCache();

    services.AddMarten(opts =>
        opts.Connection(configuration.GetConnectionString("Database")!)
    ).UseLightweightSessions();

    // Services
    services.AddScoped<ITokenService, TokenService>();

    // Database Initialization (Only in Development)
    if (env.IsDevelopment())
    {
        services.InitializeMartenWith<AuthInitialData>();
    }

    // Exception Handler
    services.AddExceptionHandler<CustomExceptionHandler>();

    // Health Checks
    services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Database")!);

    // Authentication
    var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>()!;
    var googleAuthSettings = configuration.GetSection("Authentication:Google").Get<GoogleAuthSettings>()!;
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));

    services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddCookie().AddGoogle(options =>
    {
        options.ClientId = googleAuthSettings.ClientId;
        options.ClientSecret = googleAuthSettings.ClientSecret;
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.SaveTokens = true;
    })
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
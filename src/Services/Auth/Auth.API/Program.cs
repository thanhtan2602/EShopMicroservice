using Auth.API.Repositories.Interfaces;
using Auth.API.Services;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services, builder.Configuration, builder.Environment);

var app = builder.Build();

// Configure the HTTP request pipeline.
ConfigureMiddlewares(app, builder.Environment);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
{
    var assembly = typeof(Program).Assembly;

    services.AddDbContext<AuthDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

    services.AddMediatR(config =>
    {
        config.RegisterServicesFromAssemblies(assembly);
        config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    });
    services.AddValidatorsFromAssembly(assembly);

    services.AddCarter();

    services.AddDistributedMemoryCache();

    services.Configure<AuthSettings>(configuration.GetSection("Authentication"));


    // Repositories
    services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();

    // Services
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<ITokenService, TokenService>();
    

    // Authentication
    var authSettings = configuration.GetSection("Authentication").Get<AuthSettings>()!;
    var jwtSettings = authSettings.Jwt;
    var googleAuthSettings = authSettings.Google;

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

    services.AddAuthorization();

    // Cross-Cutting Services
    builder.Services.AddExceptionHandler<CustomExceptionHandler>();

    // CORS Configuration
    services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend", policy =>
        {
            policy.WithOrigins("http://localhost:3001")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
    });
}

void ConfigureMiddlewares(WebApplication app, IWebHostEnvironment env)
{
    app.UseCors("AllowFrontend");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapCarter();
    app.UseExceptionHandler(otps => { });
}
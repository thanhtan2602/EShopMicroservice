using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Load Assembly
var assembly = typeof(Program).Assembly;

// Configure Services
ConfigureServices(builder.Services, builder.Configuration, assembly, builder.Environment);

var app = builder.Build();

// Configure Middleware
ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration, Assembly assembly, IWebHostEnvironment env)
{
    // MediatR with Behaviors
    services.AddMediatR(config =>
    {
        config.RegisterServicesFromAssembly(assembly);
        config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    });

    // Validators
    services.AddValidatorsFromAssembly(assembly);

    // Carter (Minimal API Framework)
    services.AddCarter();

    // Marten (PostgreSQL ORM)
    services.AddMarten(opts =>
    {
        opts.Connection(configuration.GetConnectionString("Database")!);
    }).UseLightweightSessions();

    // Database Initialization (Only in Development)
    if (env.IsDevelopment())
    {
        services.InitializeMartenWith<CatalogInitialData>();
    }

    // Exception Handler
    services.AddExceptionHandler<CustomExceptionHandler>();

    // Health Checks
    services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Database")!);

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
    app.UseExceptionHandler(options => { });
    app.UseHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    app.MapCarter();
}

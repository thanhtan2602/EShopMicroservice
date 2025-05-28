using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using BuildingBlocks.Messaging.MassTransit;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration, builder.Environment);

var app = builder.Build();

ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
{
    // Application Services
    var assembly = typeof(Program).Assembly;
    builder.Services.AddCarter();
    builder.Services.AddMediatR(config =>
    {
        config.RegisterServicesFromAssemblies(assembly);
        config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    });


    // Data service
    builder.Services.AddMarten(otps =>
    {
        otps.Connection(builder.Configuration.GetConnectionString("Database")!);
        otps.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    }).UseLightweightSessions();

    builder.Services.AddScoped<IBasketRepository, BasketRepository>();
    builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = builder.Configuration.GetConnectionString("Redis");
    });

    // Grpc Services
    builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
    {
        options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
    })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        return handler;
    });

    // Async Communication Services
    builder.Services.AddMessageBroker(builder.Configuration);

    // Cross-Cutting Services
    builder.Services.AddExceptionHandler<CustomExceptionHandler>();

    builder.Services.AddHealthChecks()
        .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
        .AddRedis(builder.Configuration.GetConnectionString("Redis")!);
}

void ConfigureMiddleware(WebApplication app)
{
    app.MapCarter();
    app.UseExceptionHandler(otps => { });
    app.UseHealthChecks("/health",
        new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        });
}
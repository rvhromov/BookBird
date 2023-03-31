using BookBird.Application;
using BookBird.Infrastructure;
using BookBird.Infrastructure.Extensions;
using BookBird.Jobs;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var host = Host
    .CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, builder) =>
    {
        builder
            .SetEnvironment(context, args)
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .AddCommandLine(args);
    })
    .ConfigureServices((builder, services) =>
    {
        services
            .AddApplication(builder.Configuration)
            .AddInfrastructure(builder.Configuration)
            .AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                
                x.AddConsumers();
                x.UseRabbit(builder.Configuration);
            })
            .AddQuartzServices(builder.Configuration);
        
        services.AddHttpContextAccessor();
    })
    .UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration))
    .Build();

Log.Logger = new LoggerConfiguration().CreateLogger();

await host.RunWithLoggingAsync();
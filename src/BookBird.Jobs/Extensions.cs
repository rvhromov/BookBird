using System;
using System.Threading.Tasks;
using BookBird.Application.Helpers;
using BookBird.Application.Options;
using BookBird.Infrastructure;
using BookBird.Infrastructure.Extensions;
using BookBird.Infrastructure.PipelineBehaviors;
using BookBird.Jobs.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Serilog;

namespace BookBird.Jobs
{
    public static class Extensions
    {
        public static async Task RunWithLoggingAsync(this IHost host)
        {
            Log.Logger = new LoggerConfiguration().CreateLogger();

            try
            {
                Log.Information("Jobs starting!");
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Jobs failed to start!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        
        public static IConfigurationBuilder SetEnvironment(
            this IConfigurationBuilder builder, HostBuilderContext context, string[] args)
        {
            var root = builder
                .AddCommandLine(args)
                .Build();

            var environment = root.GetValue<string>("DOTNET_ENVIRONMENT");

            if (!string.IsNullOrWhiteSpace(environment))
            {
                context.HostingEnvironment.EnvironmentName = environment;
            }

            return builder;
        }
        
        public static IBusRegistrationConfigurator UseRabbit(
            this IBusRegistrationConfigurator busConfig, IConfiguration configuration)
        {
            var options = configuration.GetOptions<RabbitMqOptions>(nameof(RabbitMqOptions));
            
            busConfig.UsingRabbitMq((context, config) =>
            {
                config.Host(options.Host, options.VirtualHost, h => 
                {
                    h.Username(options.UserName);
                    h.Password(options.Password);
                });

                config.UseConsumeFilter(typeof(ConsumerLoggingBehavior<>), context);
                config.AddConsumerEndpoints(context, configuration);
            });

            return busConfig;
        }

        public static IServiceCollection AddQuartzServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddQuartz(q =>
                {
                    q.UseMicrosoftDependencyInjectionJobFactory();
                    
                    var cronSchedulers = configuration.GetOptions<CronSchedulersOptions>(nameof(CronSchedulersOptions));
                    q.AddBookIndexingTrigger(cronSchedulers);
                })
                .AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
            
            return services;
        }
    }
}
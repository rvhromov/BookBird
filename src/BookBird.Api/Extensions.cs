using System;
using BookBird.Api.Filters;
using BookBird.Api.JsonConverters;
using BookBird.Api.Middlewares;
using BookBird.Application.Helpers;
using BookBird.Application.Options;
using BookBird.Domain.Enumerations;
using BookBird.Infrastructure.PipelineBehaviors;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BookBird.Api
{
    public static class Extensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddScoped<ExceptionMiddleware>();
            services.AddScoped<CorrelationIdMiddleware>();
            services.AddHttpContextAccessor();
            
            return services;
        }

        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<CorrelationIdMiddleware>();
            
            return app;
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

                config.UseSendFilter(typeof(MessageCorrelationIdBehavior<>), context);
                config.ConfigureEndpoints(context);
            });

            return busConfig;
        }

        public static JsonOptions AddCustomConverters(this JsonOptions options)
        {
            options.JsonSerializerOptions.Converters.Add(new MeetingTypeJsonConverter());

            return options;
        }

        public static SwaggerGenOptions AddCustomTypeMappings(this SwaggerGenOptions options)
        {
            options.MapType(typeof(MeetingType), () => new OpenApiSchema
            {
                Type = "integer",
                Example = new OpenApiInteger(0)
            });
            
            return options;
        }

        public static SwaggerGenOptions AddCustomFilters(this SwaggerGenOptions options)
        {
            options.OperationFilter<PropertyIgnoringFilter>();
            
            return options;
        }

        public static WebApplication RunWithLogging(this WebApplication app)
        {
            Log.Logger = new LoggerConfiguration().CreateLogger();

            try
            {
                Log.Information("Api starting!");
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Api failed to start!");
            }
            finally
            {
                Log.CloseAndFlush();
            }

            return app;
        }
    }
}
using System;
using BookBird.Application.Helpers;
using BookBird.Application.Options;
using BookBird.Application.Providers;
using BookBird.Application.Services;
using BookBird.Domain.Repositories;
using BookBird.Infrastructure.EF.ConfigAppliers;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Interceptors;
using BookBird.Infrastructure.EF.Repositories;
using BookBird.Infrastructure.MapperProfiles;
using BookBird.Infrastructure.PipelineBehaviors;
using BookBird.Infrastructure.Providers;
using BookBird.Infrastructure.QueryHandlers.Authors;
using BookBird.Infrastructure.Services;
using BookBird.Infrastructure.ValidationRules.Authors;
using Elasticsearch.Net;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using SendGrid.Extensions.DependencyInjection;

namespace BookBird.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbConfig(configuration);
            services.AddScoped<DomainEventInterceptor>();
            
            services.AddRepositories();
            services.AddReadServices();
            services.AddProviders();
            services.AddPipelineBehaviors();
            
            services.AddMediatR(typeof(GetAuthorsHandler));
            services.AddAutoMapper(typeof(AuthorProfile));
            services.AddValidatorsFromAssemblyContaining<GetAuthorsValidator>();
            services.AddElasticSearch(configuration);
            services.AddSendGridMailing(configuration);

            return services;
        }

        private static IServiceCollection AddDbConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("mssql");

            services.AddDbContext<WriteDbContext>((provider, context) =>
            {
                context
                    .AddInterceptors(provider.GetService<DomainEventInterceptor>()!)
                    .UseSqlServer(connectionString);
            });
            services.AddDbContext<ReadDbContext>(context => context.UseSqlServer(connectionString));
            services.AddScoped<EntityConfigApplier>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMeetingRepository, MeetingRepository>();
            services.AddScoped<IInvitationRepository, InvitationRepository>();
            services.AddScoped<IMeetingVisitorRepository, MeetingVisitorRepository>();
            
            return services;
        }
        
        private static IServiceCollection AddReadServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorReadService, AuthorReadService>();
            services.AddScoped<IGenreReadService, GenreReadService>();
            services.AddScoped<IBookReadService, BookReadService>();
            services.AddScoped<IUserReadService, UserReadService>();
            services.AddScoped<IInvitationReadService, InvitationReadService>();
            
            return services;
        }
        
        private static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddScoped<ISearchProvider, SearchProvider>();
            services.AddScoped<IEmailProvider, EmailProvider>();
            
            return services;
        }
        
        private static IServiceCollection AddPipelineBehaviors(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(HandlerLoggingBehavior<,>));
            
            return services;
        }

        private static IServiceCollection AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var elasticOptions = configuration.GetOptions<ElasticOptions>(nameof(ElasticOptions));

            var pool = new SingleNodeConnectionPool(new Uri(elasticOptions.Url));

            var connectionSettings = new ConnectionSettings(pool)
                .DefaultIndex(elasticOptions.Index)
                .RequestTimeout(TimeSpan.FromSeconds(elasticOptions.RequestTimeoutSec));

            if (elasticOptions.EnableDebugMode)
            {
                connectionSettings = connectionSettings.EnableDebugMode();
            }

            var client = new ElasticClient(connectionSettings);
            services.AddSingleton<IElasticClient>(client);

            return services;
        }

        private static IServiceCollection AddSendGridMailing(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSendGrid(opt =>
            {
                var sendGridOptions = configuration.GetOptions<SendGridOptions>(nameof(SendGridOptions));
                
                opt.ApiKey = sendGridOptions.ApiKey;
            });

            return services;
        }
    }
}
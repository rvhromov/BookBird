using BookBird.Application.Helpers;
using BookBird.Application.Options;
using BookBird.Infrastructure.IntegrationEventHandlers;
using BookBird.Infrastructure.IntegrationEventHandlers.Users;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace BookBird.Infrastructure.Extensions
{
    public static class MassTransitExtensions
    {
        public static IBusRegistrationConfigurator AddConsumers(this IBusRegistrationConfigurator busConfigurator)
        {
            busConfigurator.AddConsumers(typeof(UserCreatedConsumer).Assembly);

            return busConfigurator;
        }
        
        public static IRabbitMqBusFactoryConfigurator AddConsumerEndpoints(
            this IRabbitMqBusFactoryConfigurator busFactoryConfig, 
            IBusRegistrationContext context,
            IConfiguration configuration)
        {
            var endpointOptions = configuration.GetOptions<MassTransitEndpointsOptions>(nameof(MassTransitEndpointsOptions));
            
            busFactoryConfig
                .AddUserCreatedEndpoint(context, endpointOptions)
                .AddMeetingUpdatedEndpoint(context, endpointOptions)
                .AddMeetingDeletedEndpoint(context, endpointOptions)
                .AddInvitationAcceptedEndpoint(context, endpointOptions)
                .AddInvitationCreatedEndpoint(context, endpointOptions)
                .AddInvitationDeletedEndpoint(context, endpointOptions);
            
            return busFactoryConfig;
        }
    }
}
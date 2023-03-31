using BookBird.Application.Options;
using BookBird.Infrastructure.IntegrationEventHandlers.Invitations;
using BookBird.Infrastructure.IntegrationEventHandlers.Meetings;
using BookBird.Infrastructure.IntegrationEventHandlers.Users;
using MassTransit;

namespace BookBird.Infrastructure.IntegrationEventHandlers
{
    internal static class ReceiveEndpoints
    {
        public static IRabbitMqBusFactoryConfigurator AddUserCreatedEndpoint(
            this IRabbitMqBusFactoryConfigurator configurator, 
            IRegistrationContext context, 
            MassTransitEndpointsOptions options)
        {    
            configurator.ReceiveEndpoint(options.UserCreatedQueue, endpoint =>
            {
                endpoint.ConfigureConsumer<UserCreatedConsumer>(context);
            });

            return configurator;
        }
        
        public static IRabbitMqBusFactoryConfigurator AddMeetingUpdatedEndpoint(
            this IRabbitMqBusFactoryConfigurator configurator, 
            IRegistrationContext context, 
            MassTransitEndpointsOptions options)
        {  
            configurator.ReceiveEndpoint(options.MeetingUpdatedQueue, endpoint =>
            {
                endpoint.ConfigureConsumer<MeetingUpdatedConsumer>(context);
            });

            return configurator;
        }
        
        public static IRabbitMqBusFactoryConfigurator AddMeetingDeletedEndpoint(
            this IRabbitMqBusFactoryConfigurator configurator, 
            IRegistrationContext context, 
            MassTransitEndpointsOptions options)
        {  
            configurator.ReceiveEndpoint(options.MeetingDeletedQueue, endpoint =>
            {
                endpoint.ConfigureConsumer<MeetingDeletedConsumer>(context);
            });

            return configurator;
        }
        
        public static IRabbitMqBusFactoryConfigurator AddInvitationAcceptedEndpoint(
            this IRabbitMqBusFactoryConfigurator configurator, 
            IRegistrationContext context, 
            MassTransitEndpointsOptions options)
        {  
            configurator.ReceiveEndpoint(options.InvitationAcceptedQueue, endpoint =>
            {
                endpoint.ConfigureConsumer<InvitationAcceptedConsumer>(context);
            });

            return configurator;
        }
        
        public static IRabbitMqBusFactoryConfigurator AddInvitationCreatedEndpoint(
            this IRabbitMqBusFactoryConfigurator configurator, 
            IRegistrationContext context, 
            MassTransitEndpointsOptions options)
        {  
            configurator.ReceiveEndpoint(options.InvitationCreatedQueue, endpoint =>
            {
                endpoint.ConfigureConsumer<InvitationCreatedConsumer>(context);
            });

            return configurator;
        }
        
        public static IRabbitMqBusFactoryConfigurator AddInvitationDeletedEndpoint(
            this IRabbitMqBusFactoryConfigurator configurator, 
            IRegistrationContext context, 
            MassTransitEndpointsOptions options)
        {  
            configurator.ReceiveEndpoint(options.InvitationDeletedQueue, endpoint =>
            {
                endpoint.ConfigureConsumer<InvitationDeletedConsumer>(context);
            });

            return configurator;
        }
    }
}
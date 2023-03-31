using System;
using BookBird.Application.Helpers;
using BookBird.Application.IntegrationEvents.Invitations;
using BookBird.Application.IntegrationEvents.Meetings;
using BookBird.Application.IntegrationEvents.Users;
using BookBird.Application.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace BookBird.Application.IntegrationEvents
{
    public static class EndpointConventions
    {
        public static void AddConventions(IConfiguration configuration)
        {
            var options = configuration.GetOptions<MassTransitEndpointsOptions>(nameof(MassTransitEndpointsOptions));
            
            EndpointConvention.Map<UserCreatedIntegrationEvent>(new Uri(options.UserCreatedQueue));
            
            EndpointConvention.Map<MeetingUpdatedIntegrationEvent>(new Uri(options.MeetingUpdatedQueue));
            EndpointConvention.Map<MeetingDeletedIntegrationEvent>(new Uri(options.MeetingDeletedQueue));
            
            EndpointConvention.Map<InvitationAcceptedIntegrationEvent>(new Uri(options.InvitationAcceptedQueue));
            EndpointConvention.Map<InvitationCreatedIntegrationEvent>(new Uri(options.InvitationCreatedQueue));
            EndpointConvention.Map<InvitationDeletedIntegrationEvent>(new Uri(options.InvitationDeletedQueue));
        }
    }
}
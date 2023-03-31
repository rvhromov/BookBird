using System;

namespace BookBird.Application.IntegrationEvents.Users
{
    public sealed record UserCreatedIntegrationEvent(Guid Id);
}
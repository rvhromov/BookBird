namespace BookBird.Application.Options
{
    public class MassTransitEndpointsOptions
    {
        public string UserCreatedQueue { get; set; }
        public string MeetingUpdatedQueue { get; set; }
        public string MeetingDeletedQueue { get; set; }
        public string InvitationAcceptedQueue { get; set; }
        public string InvitationCreatedQueue { get; set; }
        public string InvitationDeletedQueue { get; set; }
    }
}
using BookBird.Domain.Entities;
using BookBird.Infrastructure.EF.Configs;
using BookBird.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.EF.ConfigAppliers
{
    internal sealed class EntityConfigApplier
    {
        private readonly AuthorConfig _authorConfig;
        private readonly GenreConfig _genreConfig;
        private readonly BookConfig _bookConfig;
        private readonly FeedbackConfig _feedbackConfig;
        private readonly UserConfig _userConfig;
        private readonly MeetingConfig _meetingConfig;
        private readonly InvitationConfig _invitationConfig;
        private readonly MeetingVisitorConfig _meetingVisitorConfig;

        public EntityConfigApplier()
        {
            _authorConfig = new AuthorConfig();
            _genreConfig = new GenreConfig();
            _bookConfig = new BookConfig();
            _feedbackConfig = new FeedbackConfig();
            _userConfig = new UserConfig();
            _meetingConfig = new MeetingConfig();
            _invitationConfig = new InvitationConfig();
            _meetingVisitorConfig = new MeetingVisitorConfig();
        }

        public void ApplyWriteDbContextConfigs(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Author>(_authorConfig);
            modelBuilder.ApplyConfiguration<Genre>(_genreConfig);
            modelBuilder.ApplyConfiguration<Book>(_bookConfig);
            modelBuilder.ApplyConfiguration<Feedback>(_feedbackConfig);
            modelBuilder.ApplyConfiguration<User>(_userConfig);
            modelBuilder.ApplyConfiguration<Meeting>(_meetingConfig);
            modelBuilder.ApplyConfiguration<Invitation>(_invitationConfig);
            modelBuilder.ApplyConfiguration<MeetingVisitor>(_meetingVisitorConfig);
        }
        
        public void ApplyReadDbContextConfigs(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<AuthorReadModel>(_authorConfig);
            modelBuilder.ApplyConfiguration<GenreReadModel>(_genreConfig);
            modelBuilder.ApplyConfiguration<BookReadModel>(_bookConfig);
            modelBuilder.ApplyConfiguration<FeedbackReadModel>(_feedbackConfig);
            modelBuilder.ApplyConfiguration<UserReadModel>(_userConfig);
            modelBuilder.ApplyConfiguration<MeetingReadModel>(_meetingConfig);
            modelBuilder.ApplyConfiguration<InvitationReadModel>(_invitationConfig);
            modelBuilder.ApplyConfiguration<MeetingVisitorReadModel>(_meetingVisitorConfig);
        }
    }
}
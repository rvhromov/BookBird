using BookBird.Infrastructure.EF.ConfigAppliers;
using BookBird.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.EF.Contexts
{
    internal sealed class ReadDbContext : DbContext
    {
        private readonly EntityConfigApplier _entityConfigApplier;
        
        public ReadDbContext(DbContextOptions<ReadDbContext> options, EntityConfigApplier entityConfigApplier) : base(options)
        {
            _entityConfigApplier = entityConfigApplier;
        }
        
        public DbSet<AuthorReadModel> Authors { get; set; }
        public DbSet<GenreReadModel> Genres { get; set; }
        public DbSet<BookReadModel> Books { get; set; }
        public DbSet<FeedbackReadModel> Feedbacks { get; set; }
        public DbSet<UserReadModel> Users { get; set; }
        public DbSet<MeetingReadModel> Meetings { get; set; }
        public DbSet<InvitationReadModel> Invitations { get; set; }
        public DbSet<MeetingVisitorReadModel> MeetingVisitors { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _entityConfigApplier.ApplyReadDbContextConfigs(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
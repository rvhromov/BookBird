using BookBird.Domain.Entities;
using BookBird.Infrastructure.EF.ConfigAppliers;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.EF.Contexts
{
    internal sealed class WriteDbContext : DbContext
    {
        private readonly EntityConfigApplier _entityConfigApplier;
        
        public WriteDbContext(DbContextOptions<WriteDbContext> options, EntityConfigApplier entityConfigApplier) : base(options)
        {
            _entityConfigApplier = entityConfigApplier;
        }
        
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<MeetingVisitor> MeetingVisitors  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            _entityConfigApplier.ApplyWriteDbContextConfigs(modelBuilder);
        }
    }
}
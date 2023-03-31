using BookBird.Domain.Entities;
using BookBird.Infrastructure.EF.Configs.Base;
using BookBird.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBird.Infrastructure.EF.Configs
{
    internal class MeetingVisitorConfig : BaseEntityTypeConfiguration<MeetingVisitor>, IEntityTypeConfiguration<MeetingVisitorReadModel>
    {
        private const string MeetingVisitorsTableName = "MeetingVisitors";
    
        protected override void ConfigureEntity(EntityTypeBuilder<MeetingVisitor> builder)
        {
            base.ConfigureEntity(builder);
            builder.ToTable(MeetingVisitorsTableName);

            builder
                .HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<MeetingVisitor>(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.Meeting)
                .WithMany(x => x.Visitors)
                .HasForeignKey(x => x.MeetingId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public void Configure(EntityTypeBuilder<MeetingVisitorReadModel> builder)
        {
            builder.ToTable(MeetingVisitorsTableName);
            
            builder
                .HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<MeetingVisitorReadModel>(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder
                .HasOne(x => x.Meeting)
                .WithMany(x => x.Visitors)
                .HasForeignKey(x => x.MeetingId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
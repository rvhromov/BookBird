using BookBird.Domain.Entities;
using BookBird.Infrastructure.EF.Configs.Base;
using BookBird.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBird.Infrastructure.EF.Configs
{
    internal class InvitationConfig : BaseEntityTypeConfiguration<Invitation>, IEntityTypeConfiguration<InvitationReadModel>
    {
        private const string InvitationTableName = "Invitations";
        
        protected override void ConfigureEntity(EntityTypeBuilder<Invitation> builder)
        {
            base.ConfigureEntity(builder);
            builder.ToTable(InvitationTableName);

            builder
                .HasOne(x => x.Meeting)
                .WithMany(x => x.Invitations)
                .HasForeignKey(x => x.MeetingId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Invitations)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public void Configure(EntityTypeBuilder<InvitationReadModel> builder)
        {
            builder.ToTable(InvitationTableName);
            
            builder
                .HasOne(x => x.Meeting)
                .WithMany(x => x.Invitations)
                .HasForeignKey(x => x.MeetingId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Invitations)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
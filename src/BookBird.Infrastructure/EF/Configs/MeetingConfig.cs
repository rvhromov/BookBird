using System;
using BookBird.Domain.Entities;
using BookBird.Domain.Enumerations;
using BookBird.Domain.Primitives;
using BookBird.Domain.ValueObjects.Meeting;
using BookBird.Infrastructure.EF.Configs.Base;
using BookBird.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookBird.Infrastructure.EF.Configs
{
    internal sealed class MeetingConfig : BaseEntityTypeConfiguration<Meeting>, IEntityTypeConfiguration<MeetingReadModel>
    {
        private const string MeetingTableName = "Meetings";
        
        protected override void ConfigureEntity(EntityTypeBuilder<Meeting> builder)
        {
            base.ConfigureEntity(builder);
            builder.ToTable(MeetingTableName);

            builder
                .Property(x => x.Name)
                .HasConversion(new ValueConverter<MeetingName, string>(n => n.Value, n => new MeetingName(n)));
            
            builder
                .Property(x => x.Location)
                .HasConversion(new ValueConverter<MeetingLocation, string>(l => l.Value, l => new MeetingLocation(l)));
            
            builder
                .Property(x => x.ScheduledFor)
                .HasColumnType("datetime2")
                .HasConversion(new ValueConverter<MeetingScheduledFor, DateTime>(s => s.Value, s => new MeetingScheduledFor(s)));
            
            builder
                .Property(x => x.Type)
                .HasConversion(new ValueConverter<MeetingType, int>(s => s.Value, s => Enumeration.FromValue<MeetingType>(s)));

            builder
                .Property(x => x.MaxNumberOfVisitors)
                .HasConversion(new ValueConverter<MeetingMaxNumberOfVisitors,int?>(n => n.Value, n => new MeetingMaxNumberOfVisitors(n)));
            
            builder
                .Property(x => x.CurrentNumberOfVisitors)
                .HasConversion(new ValueConverter<MeetingCurrentNumberOfVisitors,int>(n => n.Value, n => new MeetingCurrentNumberOfVisitors(n)));

            builder
                .HasOne(x => x.Owner)
                .WithMany(x => x.OwnMeetings)
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.Invitations)
                .WithOne(x => x.Meeting)
                .HasForeignKey(x => x.MeetingId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder
                .HasMany(x => x.Visitors)
                .WithOne(x => x.Meeting)
                .HasForeignKey(x => x.MeetingId)
                .OnDelete(DeleteBehavior.NoAction);
        }
        
        public void Configure(EntityTypeBuilder<MeetingReadModel> builder)
        {
            builder.ToTable(MeetingTableName);

            builder
                .Property(x => x.ScheduledFor)
                .HasColumnType("datetime2");
            
            builder
                .Property(x => x.Type)
                .HasConversion(new ValueConverter<MeetingType, int>(s => s.Value, s => Enumeration.FromValue<MeetingType>(s)));
            
            builder
                .HasOne(x => x.Owner)
                .WithMany(x => x.OwnMeetings)
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.Invitations)
                .WithOne(x => x.Meeting)
                .HasForeignKey(x => x.MeetingId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder
                .HasMany(x => x.Visitors)
                .WithOne(x => x.Meeting)
                .HasForeignKey(x => x.MeetingId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
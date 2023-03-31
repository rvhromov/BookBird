using BookBird.Domain.Entities;
using BookBird.Domain.ValueObjects.User;
using BookBird.Infrastructure.EF.Configs.Base;
using BookBird.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookBird.Infrastructure.EF.Configs
{
    internal sealed class UserConfig : BaseEntityTypeConfiguration<User>, IEntityTypeConfiguration<UserReadModel>
    {
        private const string UserTableName = "Users";
        
        protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
        {
            base.ConfigureEntity(builder);
            builder.ToTable(UserTableName);

            builder
                .Property(x => x.Name)
                .HasConversion(new ValueConverter<UserName, string>(n => n.Value, n => new UserName(n)));
            
            builder
                .Property(x => x.Email)
                .HasConversion(new ValueConverter<UserEmail, string>(e => e.Value, e => new UserEmail(e)));

            builder
                .HasMany(x => x.OwnMeetings)
                .WithOne(x => x.Owner)
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.Invitations)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
        
        public void Configure(EntityTypeBuilder<UserReadModel> builder)
        {
            builder.ToTable(UserTableName);
            builder.HasKey(x => x.Id);
            
            builder
                .HasMany(x => x.OwnMeetings)
                .WithOne(x => x.Owner)
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.Invitations)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
using BookBird.Domain.Entities;
using BookBird.Domain.ValueObjects.Feedback;
using BookBird.Infrastructure.EF.Configs.Base;
using BookBird.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookBird.Infrastructure.EF.Configs
{
    internal sealed class FeedbackConfig : BaseEntityTypeConfiguration<Feedback>, IEntityTypeConfiguration<FeedbackReadModel>
    {
        private const string FeedbackTableName = "Feedbacks";

        protected override void ConfigureEntity(EntityTypeBuilder<Feedback> builder)
        {
            base.ConfigureEntity(builder);
            builder.ToTable(FeedbackTableName);
            
            builder
                .Property(x => x.Description)
                .HasConversion(new ValueConverter<FeedbackDescription, string>(x => x.Value, x => new FeedbackDescription(x)));

            builder
                .Property(x => x.Rating)
                .HasConversion(new ValueConverter<FeedbackRating, ushort>(x => x.Value, x => new FeedbackRating(x)));

            builder
                .HasOne(x => x.Book)
                .WithMany(x => x.Feedbacks)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public void Configure(EntityTypeBuilder<FeedbackReadModel> builder)
        {
            builder.ToTable(FeedbackTableName);
            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.Book)
                .WithMany(x => x.Feedbacks)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
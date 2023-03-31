using System.Collections.Generic;
using BookBird.Domain.Entities;
using BookBird.Domain.ValueObjects.Book;
using BookBird.Infrastructure.EF.Configs.Base;
using BookBird.Infrastructure.EF.Models;
using BookBird.Infrastructure.EF.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookBird.Infrastructure.EF.Configs
{
    internal sealed class BookConfig : BaseEntityTypeConfiguration<Book>, IEntityTypeConfiguration<BookReadModel>
    {
        private const string BookTableName = "Books";
        private const string BooksGenresTableName = "BooksGenres";
        
        protected override void ConfigureEntity(EntityTypeBuilder<Book> builder)
        {
            base.ConfigureEntity(builder);
            builder.ToTable(BookTableName);

            builder
                .Property(x => x.Name)
                .HasConversion(new ValueConverter<BookName, string>(n => n.Value, n => new BookName(n)));
            
            builder
                .Property(x => x.PublishYear)
                .HasConversion(new ValueConverter<BookPublishYear, ushort>(n => n.Value, n => new BookPublishYear(n)));
            
            builder
                .Property(x => x.Rating)
                .HasConversion(new ValueConverter<BookRating, double>(n => n.Value, n => new BookRating(n)))
                .HasDefaultValue(default);

            builder
                .HasOne(x => x.Author)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.Genres)
                .WithMany(x => x.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookGenre",
                    right => right.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
                    left => left.HasOne<Book>().WithMany().HasForeignKey("BookId"),
                    join =>
                    {
                        join.HasKey("BookId", "GenreId");
                        join.ToTable(BooksGenresTableName);
                    });
            
            builder
                .HasMany(x => x.Feedbacks)
                .WithOne(x => x.Book)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public void Configure(EntityTypeBuilder<BookReadModel> builder)
        {
            builder.ToTable(BookTableName);
            builder.HasKey(x => x.Id);
            
            builder
                .HasOne(x => x.Author)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);;
            
            builder
                .HasMany(x => x.Genres)
                .WithMany(x => x.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookGenre",
                    right => right.HasOne<GenreReadModel>().WithMany().HasForeignKey("GenreId"),
                    left => left.HasOne<BookReadModel>().WithMany().HasForeignKey("BookId"),
                    join =>
                    {
                        join.HasKey("BookId", "GenreId");
                        join.ToTable(BooksGenresTableName);
                        join.HasData(BooksGenresSeed.GetSeed());
                    });

            builder
                .HasMany(x => x.Feedbacks)
                .WithOne(x => x.Book)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(BookSeed.GetSeed());
        }
    }
}
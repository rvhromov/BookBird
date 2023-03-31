using BookBird.Domain.Entities;
using BookBird.Domain.ValueObjects.Genre;
using BookBird.Infrastructure.EF.Configs.Base;
using BookBird.Infrastructure.EF.Models;
using BookBird.Infrastructure.EF.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookBird.Infrastructure.EF.Configs
{
    internal sealed class GenreConfig : BaseEntityTypeConfiguration<Genre>, IEntityTypeConfiguration<GenreReadModel>
    {
        private const string GenreTableName = "Genres";
        private const string BooksGenresTableName = "BooksGenres";
        
        protected override void ConfigureEntity(EntityTypeBuilder<Genre> builder)
        {
            base.ConfigureEntity(builder);
            builder.ToTable(GenreTableName);

            builder
                .Property(x => x.Name)
                .HasConversion(new ValueConverter<GenreName, string>(n => n.Value, n => new GenreName(n)));
            
            builder
                .Property(x => x.Description)
                .HasConversion(new ValueConverter<GenreDescription, string>(n => n.Value, n => new GenreDescription(n)));

            builder
                .HasMany(x => x.Books)
                .WithMany(x => x.Genres)
                .UsingEntity(x => x.ToTable(BooksGenresTableName));
        }

        public void Configure(EntityTypeBuilder<GenreReadModel> builder)
        {
            builder.ToTable(GenreTableName);
            builder.HasKey(x => x.Id);

            builder
                .HasMany(x => x.Books)
                .WithMany(x => x.Genres)
                .UsingEntity(x => x.ToTable(BooksGenresTableName));

            builder.HasData(GenreSeed.GetSeed());
        }
    }
}
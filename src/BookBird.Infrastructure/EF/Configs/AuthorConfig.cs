using BookBird.Domain.Entities;
using BookBird.Domain.ValueObjects.Author;
using BookBird.Infrastructure.EF.Configs.Base;
using BookBird.Infrastructure.EF.Models;
using BookBird.Infrastructure.EF.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookBird.Infrastructure.EF.Configs
{
    internal sealed class AuthorConfig : BaseEntityTypeConfiguration<Author>, IEntityTypeConfiguration<AuthorReadModel>
    {
        private const string AuthorTableName = "Authors";
        
        protected override void ConfigureEntity(EntityTypeBuilder<Author> builder)
        {
            base.ConfigureEntity(builder);
            builder.ToTable(AuthorTableName);

            builder
                .Property(x => x.FirstName)
                .HasConversion(new ValueConverter<AuthorFirstName, string>(n => n.Value, n => new AuthorFirstName(n)));

            builder
                .Property(x => x.LastName)
                .HasConversion(new ValueConverter<AuthorLastName, string>(n => n.Value, n => new AuthorLastName(n)));

            builder
                .HasMany(x => x.Books)
                .WithOne(x => x.Author)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);;
        }

        public void Configure(EntityTypeBuilder<AuthorReadModel> builder)
        {
            builder.ToTable(AuthorTableName);
            builder.HasKey(x => x.Id);

            builder
                .HasMany(x => x.Books)
                .WithOne(x => x.Author)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(AuthorSeed.GetSeed());
        }
    }
}
using System;
using System.Linq.Expressions;
using BookBird.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBird.Infrastructure.EF.Configs.Base
{
    internal abstract class BaseEntityTypeConfiguration<TEntity> : BaseTypeConfiguration<TEntity> where TEntity : Entity
    {
        protected override Expression<Func<TEntity, object>> Key => entity => entity.Id;

        protected override void ConfigureEntity(EntityTypeBuilder<TEntity> builder)
        {
            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();
            
            builder
                .Property(x => x.CreatedAt)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(x => x.ModifiedAt)
                .HasColumnType("datetime2");

            builder.Ignore(x => x.DomainEvents);
        }
    }
}
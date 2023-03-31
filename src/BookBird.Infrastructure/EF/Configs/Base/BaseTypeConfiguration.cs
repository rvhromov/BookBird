using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBird.Infrastructure.EF.Configs.Base
{
    internal abstract class BaseTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        protected abstract Expression<Func<TEntity, object>> Key { get; }
        protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
        
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(Key);
            ConfigureEntity(builder);
        }
    }
}
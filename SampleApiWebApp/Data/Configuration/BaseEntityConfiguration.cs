using EntityManagement.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SampleApiWebApp.Data.Configuration
{
    public abstract class BaseEntityConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IEntity<TId>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.CreatedBy)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(i => i.ModifiedBy)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}

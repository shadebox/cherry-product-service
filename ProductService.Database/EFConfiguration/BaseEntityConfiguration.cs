#region Include Definition
using ProductService.Database.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
#endregion

namespace ProductService.Database.EFConfiguration
{
    #region Public Class Definition
    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        #region Public Method Definition
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            // Set table types in database
            builder.Property(p => p.CreateDate).HasColumnType("datetime2").HasDefaultValueSql("GetDate()");
            builder.Property(p => p.Timestamp).IsRowVersion();
            builder.Property(p => p.Status).HasColumnType("tinyint");

            // Set type properties
            builder.HasIndex(i => i.Status).HasDatabaseName("IX_BaseEntity_Status");
        }
        #endregion
    }
    #endregion
}
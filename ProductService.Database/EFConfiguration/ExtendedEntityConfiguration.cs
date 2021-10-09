#region Include Definition
using ProductService.Database.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
#endregion

namespace ProductService.Database.EFConfiguration
{
    #region Public Class Definition
    public abstract class ExtendedEntityConfiguration<TEntity> : BaseEntityConfiguration<TEntity> where TEntity : ExtendedEntity
    {
        #region Public Method Definition
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            // Set table types in database
            builder.Property(p => p.ModifiedDate).HasColumnType("Datetime2").HasDefaultValueSql("GetDate()");
            
            base.Configure(builder);
        }
        #endregion
    }
    #endregion
}
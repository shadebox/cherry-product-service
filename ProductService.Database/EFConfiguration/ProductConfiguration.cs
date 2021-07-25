#region Include Definition
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Database.Domain;
#endregion

namespace ProductService.Database.EFConfiguration
{
    #region Public Class Definition
    public class ProductConfiguration : ExtendedEntityConfiguration<Product>
    {
        #region Public Method Definition
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            // Set table types in database
            builder.Property(p => p.ID).HasColumnName("ProductID");
            builder.Property(p => p.Name).HasColumnType("varchar");
            builder.Property(p => p.Description).HasColumnType("text");
            builder.Property(p => p.Specification).HasColumnType("text");
            builder.Property(p => p.Delivery).HasColumnType("text");
            builder.Property(p => p.ModelNumber).HasColumnType("varchar");

            // Set type properties
            builder.HasKey(k => k.ID).HasName("PK_Product");
            builder.HasIndex(i => i.Name).HasDatabaseName("IX_Product_Name");
            builder.HasIndex(i => i.ModelNumber).HasDatabaseName("IX_Product_ModelNumber");

            base.Configure(builder);
        }
        #endregion
    }
    #endregion
}
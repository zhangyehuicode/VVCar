using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    public class ProductCategoryMap : EntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.LastUpdateUser)
                .HasMaxLength(20);
        }
    }
}

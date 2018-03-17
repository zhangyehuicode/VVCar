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
            HasKey(t => t.ID);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}

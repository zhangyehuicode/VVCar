using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.ImgUrl)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.LastUpdateUser)
                .HasMaxLength(20);
        }
    }
}

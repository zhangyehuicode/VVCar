using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    public class CarBitCoinOrderItemMap : EntityTypeConfiguration<CarBitCoinOrderItem>
    {
        public CarBitCoinOrderItemMap()
        {
            HasKey(t => t.ID);

            Property(t => t.ProductName)
                .HasMaxLength(20);

            Property(t => t.ImgUrl)
                .HasMaxLength(50);
        }
    }
}

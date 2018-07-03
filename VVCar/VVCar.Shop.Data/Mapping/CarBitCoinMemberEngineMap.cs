using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    public class CarBitCoinMemberEngineMap : EntityTypeConfiguration<CarBitCoinMemberEngine>
    {
        public CarBitCoinMemberEngineMap()
        {
            HasKey(t => t.ID);

            Property(t => t.CarBitCoinProductName)
                .HasMaxLength(20);

            Property(t => t.TradeNo)
                .HasMaxLength(50);

            Property(t => t.ImgUrl)
              .HasMaxLength(50);
        }
    }
}

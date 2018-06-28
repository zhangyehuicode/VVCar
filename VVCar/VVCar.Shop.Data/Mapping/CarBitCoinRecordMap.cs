using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    public class CarBitCoinRecordMap : EntityTypeConfiguration<CarBitCoinRecord>
    {
        public CarBitCoinRecordMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Remark)
                .HasMaxLength(20);

            Property(t => t.TradeNo)
                .HasMaxLength(20);
        }
    }
}

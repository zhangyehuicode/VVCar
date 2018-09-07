using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class StockholderDividendMap : EntityTypeConfiguration<StockholderDividend>
    {
        public StockholderDividendMap()
        {
            HasKey(t => t.ID);

            Property(t => t.TradeNo)
                .HasMaxLength(20);
        }
    }
}

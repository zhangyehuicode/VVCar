using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    public class StockRecordMap : EntityTypeConfiguration<StockRecord>
    {
        public StockRecordMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Reason)
                .HasMaxLength(50);

            Property(t => t.StaffName)
                .HasMaxLength(20);
        }
    }
}

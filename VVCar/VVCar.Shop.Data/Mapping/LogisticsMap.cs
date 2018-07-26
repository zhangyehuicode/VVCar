using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    public class LogisticsMap : EntityTypeConfiguration<Logistics>
    {
        public LogisticsMap()
        {
            HasKey(t => t.ID);

            Property(t => t.RevisitTips)
                .HasMaxLength(50);

            Property(t => t.DeliveryTips)
                .HasMaxLength(50);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class CouponPushItemMap : EntityTypeConfiguration<CouponPushItem>
    {
        public CouponPushItemMap()
        {
            HasKey(t => t.ID);

            Property(t => t.CouponTemplateTitle)
             .IsRequired()
             .HasMaxLength(18);
        }
    }
}

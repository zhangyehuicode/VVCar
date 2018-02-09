using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class PointExchangeCouponMap : EntityTypeConfiguration<PointExchangeCoupon>
    {
        public PointExchangeCouponMap()
        {
            HasKey(i => i.ID);

            HasRequired(i => i.CouponTemplate)
                .WithMany()
                .HasForeignKey(i => i.CouponTemplateId);
        }
    }
}

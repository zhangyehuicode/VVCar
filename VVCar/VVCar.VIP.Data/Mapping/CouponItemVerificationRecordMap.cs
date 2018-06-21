using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class CouponItemVerificationRecordMap : EntityTypeConfiguration<CouponItemVerificationRecord>
    {
        public CouponItemVerificationRecordMap()
        {
            HasKey(t => t.ID);

            Property(t => t.TradeNo)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}

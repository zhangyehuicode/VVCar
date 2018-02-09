using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class GivenCouponRecordMap : EntityTypeConfiguration<GivenCouponRecord>
    {
        public GivenCouponRecordMap()
        {
            HasKey(t => t.ID);

            Property(t => t.OwnerOpenID)
                .HasMaxLength(50);

            Property(t => t.DonorOpenID)
                .HasMaxLength(50);

            Property(t => t.OwnerNickName)
                .HasMaxLength(100);

            Property(t => t.DonorNickName)
                .HasMaxLength(100);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class CouponTemplateUseTimeMap : EntityTypeConfiguration<CouponTemplateUseTime>
    {
        public CouponTemplateUseTimeMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.BeginTime)
                .IsRequired()
                .HasMaxLength(5);

            Property(t => t.EndTime)
                .IsRequired()
                .HasMaxLength(5);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class VerificationRecordMap : EntityTypeConfiguration<VerificationRecord>
    {
        public VerificationRecordMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.CouponCode)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.VerificationCode)
                .HasMaxLength(5);

            Property(t => t.DepartmentCode)
                .HasMaxLength(10);

            // Table Relationships
            HasRequired(t => t.Coupon)
                .WithMany()
                .HasForeignKey(t => t.CouponID);
        }
    }
}

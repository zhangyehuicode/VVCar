using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class CouponMap : EntityTypeConfiguration<Coupon>
    {
        public CouponMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.CouponCode)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.OwnerOpenID)
                .HasMaxLength(50);

            Property(t => t.MinProOpenID)
                .HasMaxLength(50);

            Property(t => t.OwnerNickName)
                .HasMaxLength(100);

            Property(t => t.OwnerPhoneNo)
                .HasMaxLength(20);

            Property(t => t.ReceiveChannel)
                .HasMaxLength(50);

            // Table Relationships
            HasRequired(t => t.Template)
                .WithMany()
                .HasForeignKey(t => t.TemplateID);
        }
    }
}

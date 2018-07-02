using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    public class CarBitCoinOrderPaymentDetailsMap : EntityTypeConfiguration<CarBitCoinOrderPaymentDetails>
    {
        public CarBitCoinOrderPaymentDetailsMap()
        {
            HasKey(t => t.ID);

            Property(t => t.CarBitCoinOrderCode)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.PayInfo)
                 .HasMaxLength(100);

            Property(t => t.MemberInfo)
                 .HasMaxLength(100);

            Property(t => t.StaffName)
                 .HasMaxLength(20);
        }
    }
}

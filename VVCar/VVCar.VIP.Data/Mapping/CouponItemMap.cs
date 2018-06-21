using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class CouponItemMap : EntityTypeConfiguration<CouponItem>
    {
        public CouponItemMap()
        {
            HasKey(t => t.ID);

            Property(t => t.ProductName)
                .HasMaxLength(20);

            Property(t => t.ProductCode)
                .HasMaxLength(20);
        }
    }
}

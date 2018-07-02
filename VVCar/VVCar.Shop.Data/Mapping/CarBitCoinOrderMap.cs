using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    public class CarBitCoinOrderMap : EntityTypeConfiguration<CarBitCoinOrder>
    {
        public CarBitCoinOrderMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.LinkMan)
                .HasMaxLength(20);

            Property(t => t.Phone)
                .HasMaxLength(20);

            Property(t => t.Address)
                .HasMaxLength(100);

            Property(t => t.ExpressNumber)
                .HasMaxLength(50);

            Property(t => t.Remark)
                .HasMaxLength(100);

            Property(t => t.OpenID)
                .HasMaxLength(36);

            Property(t => t.LastUpdatedUser)
                .HasMaxLength(20);
        }
    }
}

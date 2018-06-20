using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    public class CarBitCoinMemberMap : EntityTypeConfiguration<CarBitCoinMember>
    {
        public CarBitCoinMemberMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.MobilePhoneNo)
                .IsRequired()
                .HasMaxLength(11);

            Property(t => t.OpenID)
                .HasMaxLength(50);
        }
    }
}

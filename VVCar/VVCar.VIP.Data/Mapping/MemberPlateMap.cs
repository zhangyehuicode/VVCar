using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class MemberPlateMap : EntityTypeConfiguration<MemberPlate>
    {
        public MemberPlateMap()
        {
            HasKey(t => t.ID);

            Property(t => t.PlateNumber)
                .IsRequired()
                .HasMaxLength(10);

            Property(t => t.OpenID)
                .HasMaxLength(36);
        }
    }
}

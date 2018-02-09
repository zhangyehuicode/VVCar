using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class MemberGradeRightMap : EntityTypeConfiguration<MemberGradeRight>
    {
        public MemberGradeRightMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.PosRightCode)
                .HasMaxLength(50);

            this.Property(t => t.PosRightName)
                .HasMaxLength(50);
        }
    }
}

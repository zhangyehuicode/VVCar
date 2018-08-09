using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class MemberGroupMap : EntityTypeConfiguration<MemberGroup>
    {
        public MemberGroupMap()
        {
            this.HasKey(t => t.ID);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(16);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.CreatedUser)
                .HasMaxLength(20);

            this.Property(t => t.LastUpdateUser)
                .HasMaxLength(20);
        }
    }
}

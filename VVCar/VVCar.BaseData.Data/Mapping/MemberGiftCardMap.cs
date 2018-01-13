using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    public class MemberGiftCardMap : EntityTypeConfiguration<MemberGiftCard>
    {
        public MemberGiftCardMap()
        {
            HasKey(t => t.ID);

            Property(t => t.OpenID)
                .IsRequired()
                .HasMaxLength(36);

            Property(t => t.CreatedUser)
                .HasMaxLength(20);

            Property(t => t.LastUpdateUser)
                .HasMaxLength(20);

            Property(t => t.OrderNo)
                .HasMaxLength(50);

            Property(t => t.RechargePlanCode)
                .HasMaxLength(20);
        }
    }
}
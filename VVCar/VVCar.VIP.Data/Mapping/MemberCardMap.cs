using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class MemberCardMap : EntityTypeConfiguration<MemberCard>
    {
        public MemberCardMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.VerifyCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.BatchCode)
                .HasMaxLength(10);

            this.Property(t => t.CreatedUser)
                .HasMaxLength(20);

            this.Property(t => t.LastUpdateUser)
                .HasMaxLength(20);

            this.Property(t => t.Remark)
                .HasMaxLength(50);

            this.HasRequired(t => t.CardType)
                .WithMany()
                .HasForeignKey(t => t.CardTypeID);
        }
    }
}

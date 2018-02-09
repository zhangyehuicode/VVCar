using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class MemberGradeMap : EntityTypeConfiguration<MemberGrade>
    {
        public MemberGradeMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Remark)
                .HasMaxLength(500);

            this.Property(t => t.CreatedUser)
                .HasMaxLength(20);

            this.Property(t => t.LastUpdateUser)
                .HasMaxLength(20);

            // Table relationships
            this.HasMany(t => t.GradeRights)
                .WithRequired(right => right.MemberGrade)
                .HasForeignKey(t => t.MemberGradeID);
        }
    }
}

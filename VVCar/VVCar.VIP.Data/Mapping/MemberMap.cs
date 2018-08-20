using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Mapping
{
    public class MemberMap : EntityTypeConfiguration<Member>
    {
        public MemberMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.CardNumber)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MobilePhoneNo)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.WeChatOpenID)
                .HasMaxLength(36);

            this.Property(t => t.MinProOpenID)
                .HasMaxLength(36);

            this.Property(t => t.CreatedUser)
                .HasMaxLength(20);

            this.Property(t => t.LastUpdateUser)
                .HasMaxLength(20);

            this.Property(t => t.Password)
                .HasMaxLength(32);

            this.Property(t => t.PhoneLocation)
                .HasMaxLength(20);

            this.Property(t => t.DepartmentName)
                .HasMaxLength(50);

            this.Property(t => t.DepartmentAddress)
                .HasMaxLength(100);

            this.HasRequired(t => t.Card)
                .WithMany()
                .HasForeignKey(t => t.CardID);

            this.HasOptional(t => t.OwnerDepartment)
                .WithMany()
                .HasForeignKey(t => t.OwnerDepartmentID);

            //this.HasOptional(t => t.OwnerGroup)
            //    .WithMany()
            //    .HasForeignKey(t => t.MemberGroupID);

            //this.HasOptional(t => t.MemberGrade)
            //    .WithMany()
            //    .HasForeignKey(t => t.MemberGradeID);
        }
    }
}

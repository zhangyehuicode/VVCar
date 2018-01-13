using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.PhoneNo)
                .HasMaxLength(15);

            this.Property(t => t.MobilePhoneNo)
                .HasMaxLength(15);

            this.Property(t => t.EmailAddress)
                .HasMaxLength(30);

            this.Property(t => t.Remark)
                .HasMaxLength(200);

            this.Property(t => t.Password)
                .HasMaxLength(50);

            this.Property(t => t.AuthorityCard)
                .HasMaxLength(50);

            this.Property(t => t.CreatedUser)
                .HasMaxLength(20);

            this.Property(t => t.LastUpdateUser)
                .HasMaxLength(20);

            this.HasMany(t => t.UserRoles)
                .WithRequired(t => t.User)
                .HasForeignKey(t => t.UserID);

            this.HasRequired(t => t.Department)
                .WithMany()
                .HasForeignKey(t => t.DepartmentID);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    public class DepartmentMap : EntityTypeConfiguration<Department>
    {
        public DepartmentMap()
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

            this.Property(t => t.ContactPerson)
                .HasMaxLength(20);

            this.Property(t => t.ContactPhoneNo)
                .HasMaxLength(30);

            this.Property(t => t.MobilePhoneNo)
                .HasMaxLength(30);

            this.Property(t => t.EmailAddress)
                .HasMaxLength(30);

            this.Property(t => t.Address)
                .HasMaxLength(100);

            this.Property(t => t.DistrictRegion)
                .HasMaxLength(20);

            this.Property(t => t.AdministrationRegion)
                .HasMaxLength(20);

            this.Property(t => t.Remark)
                .HasMaxLength(200);

            this.Property(t => t.CreatedUser)
                .HasMaxLength(20);

            this.Property(t => t.LastUpdateUser)
                .HasMaxLength(20);
        }
    }
}

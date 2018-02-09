using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class VerificationCodeMap : EntityTypeConfiguration<VerificationCode>
    {
        public VerificationCodeMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(5);

            Property(t => t.DepartmentCode)
                .HasMaxLength(10);

            Property(t => t.DepartmentName)
                .HasMaxLength(20);

            Property(t => t.CreatedUser)
                .HasMaxLength(20);
        }
    }
}

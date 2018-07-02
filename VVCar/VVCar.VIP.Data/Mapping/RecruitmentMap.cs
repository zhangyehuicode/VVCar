using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    class RecruitmentMap : EntityTypeConfiguration<Recruitment>
    {
        public RecruitmentMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Recruiter)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.HRName)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.HRPhoneNo)
                .HasMaxLength(20);

            Property(t => t.Address)
                .HasMaxLength(20);

            Property(t => t.WorkTime)
                .HasMaxLength(30);

            Property(t => t.Requirement)
                .HasMaxLength(250);
        }
    }
}

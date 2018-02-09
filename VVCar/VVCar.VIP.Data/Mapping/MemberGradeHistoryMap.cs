using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class MemberGradeHistoryMap : EntityTypeConfiguration<MemberGradeHistory>
    {
        public MemberGradeHistoryMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            this.HasRequired(t => t.Member)
                .WithMany()
                .HasForeignKey(t => t.MemberID);
        }
    }
}

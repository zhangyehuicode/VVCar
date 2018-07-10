using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    /// <summary>
    /// 业务报销Dto
    /// </summary>
    public class ReimbursementMap : EntityTypeConfiguration<Reimbursement>
    {
        public ReimbursementMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Project)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.ImgUrl)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Remark)
                .HasMaxLength(200);

            Property(t => t.CreatedUser)
               .IsRequired()
               .HasMaxLength(20);

            Property(t => t.LastUpdateUser)
                .HasMaxLength(20);
        }
    }
}

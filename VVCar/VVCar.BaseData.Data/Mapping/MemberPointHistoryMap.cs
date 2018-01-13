using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    public class MemberPointHistoryMap : EntityTypeConfiguration<MemberPointHistory>
    {
        public MemberPointHistoryMap()
        {
            HasKey(t => t.ID);

            Property(t => t.OutTradeNo)
                .HasMaxLength(100);

            Property(t => t.Remark)
                .HasMaxLength(100);
        }
    }
}

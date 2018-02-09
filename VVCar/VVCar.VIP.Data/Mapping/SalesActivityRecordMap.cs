using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class SalesActivityRecordMap : EntityTypeConfiguration<SalesActivityRecord>
    {
        public SalesActivityRecordMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.WeChatOpenID)
                .HasMaxLength(50);

            Property(t => t.RetailBillCode)
                .HasMaxLength(50);

            Property(t => t.MobilePhoneNo)
                .HasMaxLength(20);

            Property(t => t.Prize)
                .HasMaxLength(2000);
        }
    }
}

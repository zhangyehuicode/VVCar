using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class MerchantCrowdOrderRecordMap : EntityTypeConfiguration<MerchantCrowdOrderRecord>
    {
        public MerchantCrowdOrderRecordMap()
        {
            this.HasKey(t => t.ID);
        }
    }
}

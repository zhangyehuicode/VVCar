using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class MerchantBargainOrderRecordMap : EntityTypeConfiguration<MerchantBargainOrderRecord>
    {
        public MerchantBargainOrderRecordMap()
        {
            this.HasKey(t => t.ID);
        }
    }
}

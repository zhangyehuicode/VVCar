using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class CrowdOrderRecordItemMap : EntityTypeConfiguration<CrowdOrderRecordItem>
    {
        public CrowdOrderRecordItemMap()
        {
            this.HasKey(t => t.ID);
        }
    }
}

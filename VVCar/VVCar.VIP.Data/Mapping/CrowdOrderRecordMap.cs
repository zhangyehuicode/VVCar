using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class CrowdOrderRecordMap : EntityTypeConfiguration<CrowdOrderRecord>
    {
        public CrowdOrderRecordMap()
        {
            this.HasKey(t => t.ID);
        }
    }
}

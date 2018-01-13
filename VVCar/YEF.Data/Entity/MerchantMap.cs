using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using YEF.Core.Data;

namespace YEF.Data
{
    public class MerchantMap : EntityTypeConfiguration<Merchant>
    {
        public MerchantMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}

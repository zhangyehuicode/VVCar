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
    /// 门店砍价Map
    /// </summary>
    public class MerchantBargainOrderMap : EntityTypeConfiguration<MerchantBargainOrder>
    {
        public MerchantBargainOrderMap()
        {
            HasKey(t => t.ID);
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.LastUpdateUser)
                .HasMaxLength(20);
        }

    }
}

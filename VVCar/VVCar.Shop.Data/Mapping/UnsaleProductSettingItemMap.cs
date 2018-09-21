using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    /// <summary>
    /// 产品滞销提醒配置子项MAP
    /// </summary>
    public class UnsaleProductSettingItemMap: EntityTypeConfiguration<UnsaleProductSettingItem>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public UnsaleProductSettingItemMap()
        {
            HasKey(t => t.ID);

            Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}

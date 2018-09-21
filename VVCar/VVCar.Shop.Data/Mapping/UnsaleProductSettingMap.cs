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
    /// 产品滞销提醒配置MAP
    /// </summary>
    public class UnsaleProductSettingMap : EntityTypeConfiguration<UnsaleProductSetting>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public UnsaleProductSettingMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(20);

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

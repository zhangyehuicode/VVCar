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
    public class UnsaleProductHistoryMap : EntityTypeConfiguration<UnsaleProductHistory>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public UnsaleProductHistoryMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.ProductCode)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}

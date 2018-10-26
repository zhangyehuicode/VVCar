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
    /// 员工分红Map
    /// </summary>
    public class OrderDividendMap : EntityTypeConfiguration<OrderDividend>
    {
        public OrderDividendMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Code)
                .HasMaxLength(20);

            Property(t => t.Name)
                .HasMaxLength(50);

            Property(t => t.PlateNumber)
                .HasMaxLength(10);

            Property(t => t.UserCode)
                .HasMaxLength(20);

            Property(t => t.UserName)
                .HasMaxLength(50); 
        }
    }
}

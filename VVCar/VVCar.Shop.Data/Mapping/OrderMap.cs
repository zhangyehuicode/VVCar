using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.LinkMan)
                //.IsRequired()
                .HasMaxLength(20);

            Property(t => t.Phone)
                //.IsRequired()
                .HasMaxLength(20);

            Property(t => t.Address)
                //.IsRequired()
                .HasMaxLength(100);

            Property(t => t.ExpressNumber)
                .HasMaxLength(50);

            Property(t => t.Remark)
                .HasMaxLength(100);

            Property(t => t.OpenID)
                .HasMaxLength(36);

            Property(t => t.LastUpdatedUser)
                .HasMaxLength(20);
        }
    }
}

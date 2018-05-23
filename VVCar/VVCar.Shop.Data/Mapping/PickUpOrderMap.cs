using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    public class PickUpOrderMap : EntityTypeConfiguration<PickUpOrder>
    {
        public PickUpOrderMap()
        {
            HasKey(t => t.ID);

            Property(t => t.PlateNumber)
                .IsRequired()
                .HasMaxLength(10);

            Property(t => t.StaffName)
                .HasMaxLength(20);
        }
    }
}

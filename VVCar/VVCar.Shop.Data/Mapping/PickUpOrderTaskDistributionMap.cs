using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    public class PickUpOrderTaskDistributionMap : EntityTypeConfiguration<PickUpOrderTaskDistribution>
    {
        public PickUpOrderTaskDistributionMap()
        {
            HasKey(t => t.ID);

            this.Property(t => t.CreatedUser)
                .HasMaxLength(20);

            this.Property(t => t.LastUpdateUser)
                .HasMaxLength(20);
        }
    }
}

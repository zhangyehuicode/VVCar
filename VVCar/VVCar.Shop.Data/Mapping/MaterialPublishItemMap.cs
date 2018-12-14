using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    public class MaterialPublishItemMap : EntityTypeConfiguration<MaterialPublishItem>
    {
        public MaterialPublishItemMap()
        {
            HasKey(t => t.ID);

            Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    class MaterialMap : EntityTypeConfiguration<Material>
    {
        public MaterialMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Url)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.LastUpdateUser)
                .HasMaxLength(20);
        }
    }
}

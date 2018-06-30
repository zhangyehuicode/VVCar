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
    /// 车比特产品类别Map
    /// </summary>
    public class CarBitCoinProductCategoryMap : EntityTypeConfiguration<CarBitCoinProductCategory>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public CarBitCoinProductCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            //Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.LastUpdateUser)
                .HasMaxLength(20);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    public class ServicePeriodSettingMap : EntityTypeConfiguration<ServicePeriodSetting>
    {
        public ServicePeriodSettingMap()
        {
            HasKey(t => t.ID);

            Property(t => t.ExpirationNotice)
                .HasMaxLength(20);

            Property(t => t.CreatedUser)
             .IsRequired()
             .HasMaxLength(20);
        }
    }
}

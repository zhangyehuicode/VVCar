using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    public class SystemSettingMap : EntityTypeConfiguration<SystemSetting>
    {
        public SystemSettingMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Caption)
                .HasMaxLength(50);

            this.Property(t => t.DefaultValue)
                .HasMaxLength(100);

            this.Property(t => t.SettingValue)
                .HasMaxLength(1500);

            this.Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.LastUpdateUser)
                .HasMaxLength(20);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    public class DataDictValueMap : EntityTypeConfiguration<DataDictValue>
    {
        public DataDictValueMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.DictType)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.DictValue)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.DictName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Remark)
                .HasMaxLength(200);
        }
    }
}

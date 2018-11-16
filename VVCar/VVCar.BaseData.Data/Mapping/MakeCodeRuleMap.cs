using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    public class MakeCodeRuleMap : EntityTypeConfiguration<MakeCodeRule>
    {
        public MakeCodeRuleMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Prefix1)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Prefix2)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Prefix3)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}

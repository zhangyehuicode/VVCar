using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    public class CardThemeGroupMap : EntityTypeConfiguration<CardThemeGroup>
    {
        public CardThemeGroupMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Code)
                .HasMaxLength(20);

            Property(t => t.DepartmentCode)
                .HasMaxLength(20);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(250);

            Property(t => t.Week)
                .HasMaxLength(20);

            Property(t => t.RuleDescription)
                .HasMaxLength(200);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    public class CardThemeGroupUseTimeMap : EntityTypeConfiguration<CardThemeGroupUseTime>
    {
        public CardThemeGroupUseTimeMap()
        {
            HasKey(t => t.ID);

            Property(t => t.BeginTime)
                .IsRequired()
                .HasMaxLength(10);

            Property(t => t.EndTime)
                .IsRequired()
                .HasMaxLength(10);

            HasRequired(t => t.CardThemeGroup)
                .WithMany(t => t.UseTimeList)
                .HasForeignKey(t => t.CardThemeGroupID);

        }
    }
}

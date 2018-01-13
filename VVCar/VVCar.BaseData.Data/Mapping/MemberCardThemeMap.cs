using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    public class MemberCardThemeMap : EntityTypeConfiguration<MemberCardTheme>
    {
        public MemberCardThemeMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.ImgUrl)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}

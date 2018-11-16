using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    /// <summary>
    /// 寻客侠广告配置
    /// </summary>
    public class AdvisementSettingMap : EntityTypeConfiguration<AdvisementSetting>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AdvisementSettingMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.SubTitle)
                .HasMaxLength(20);

            Property(t => t.ImgUrl)
                .HasMaxLength(50);

            Property(t => t.Content)
                .HasMaxLength(15000);

            Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.LastUpdateUser)
                .HasMaxLength(20);
        }
    }
}

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
    /// 游戏设置映射
    /// </summary>
    public class GameSettingMap : EntityTypeConfiguration<GameSetting>
    {
        /// <summary>
        /// 游戏设置映射构造函数
        /// </summary>
        public GameSettingMap()
        {
            this.HasKey(t => t.ID);

            this.Property(t => t.ShareTitle)
                .HasMaxLength(20);

            this.Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.LastUpdateUser)
                .HasMaxLength(20);
        }
    }
}

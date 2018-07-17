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
    /// 游戏推送Map
    /// </summary>
    public class GamePushMap : EntityTypeConfiguration<GamePush>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public GamePushMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(30);

            Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.LastUpdateUser)
                .HasMaxLength(20);
        }

    }
}

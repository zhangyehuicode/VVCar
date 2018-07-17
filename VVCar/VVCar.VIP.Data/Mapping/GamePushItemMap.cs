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
    /// 游戏推送子项Map
    /// </summary>
    public class GamePushItemMap : EntityTypeConfiguration<GamePushItem>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public GamePushItemMap()
        {
            HasKey(t => t.ID);

            Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}

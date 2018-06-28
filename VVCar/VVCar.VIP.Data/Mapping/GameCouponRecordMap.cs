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
    /// 游戏卡券记录Map
    /// </summary>
    public class GameCouponRecordMap : EntityTypeConfiguration<GameCouponRecord>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public GameCouponRecordMap()
        {
            //Primary Key
            HasKey(t => t.ID);

            Property(t => t.ReceiveOpenID)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.CouponTitle)
                .HasMaxLength(18);

            Property(t => t.NickName)
                .HasMaxLength(100);

            Property(t => t.OutTradeNo)
                .HasMaxLength(50);
        }
    }
}

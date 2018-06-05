using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    /// <summary>
    /// 游戏卡券配置映射
    /// </summary>
    public class GameCouponMap : EntityTypeConfiguration<GameCoupon>
    {
        /// <summary>
        /// 游戏卡券配置映射构造函数
        /// </summary>
        public GameCouponMap()
        {
            HasKey(t => t.ID);
        }
    }
}

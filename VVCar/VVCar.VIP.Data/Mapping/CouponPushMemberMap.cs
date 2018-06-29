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
    /// 卡券推送会员Map
    /// </summary>
    public class CouponPushMemberMap : EntityTypeConfiguration<CouponPushMember>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CouponPushMemberMap()
        {
            HasKey(t => t.ID);

            Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}

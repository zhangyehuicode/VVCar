using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    /// <summary>
    /// 用户会员关联Map
    /// </summary>
    public class UserMemberMap : EntityTypeConfiguration<UserMember>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public UserMemberMap()
        {
            this.HasKey(t=>t.ID);

            this.Property(t => t.WeChatOpenID)
                .HasMaxLength(36);

            this.Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}

using System.Data.Entity.ModelConfiguration;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    /// <summary>
    /// 游戏推送会员Map
    /// </summary>
    public class GamePushMemberMap: EntityTypeConfiguration<GamePushMember>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public GamePushMemberMap()
        {
            HasKey(t => t.ID);

            Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}

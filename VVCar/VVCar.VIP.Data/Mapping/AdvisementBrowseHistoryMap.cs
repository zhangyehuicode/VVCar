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
    /// 寻客侠广告浏览记录Map
    /// </summary>
    public class AdvisementBrowseHistoryMap : EntityTypeConfiguration<AdvisementBrowseHistory>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AdvisementBrowseHistoryMap()
        {
            HasKey(t => t.ID);

            Property(t => t.OpenID)
                .HasMaxLength(36);

            Property(t => t.NickName)
                .HasMaxLength(50);
        }
    }
}

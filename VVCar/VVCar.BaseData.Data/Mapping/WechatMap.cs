using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    public class WechatMap : EntityTypeConfiguration<Wechat>
    {
        public WechatMap()
        {
            this.HasKey(t => t.ID);

            this.Property(t => t.Token)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.EncodingAESKey)
                .HasMaxLength(43);

            this.Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}

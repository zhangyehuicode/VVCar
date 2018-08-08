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
    /// 图文消息子项
    /// </summary>
    public class ArticleItemMap : EntityTypeConfiguration<ArticleItem>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ArticleItemMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.ThumbMediaID)
                .HasMaxLength(50);

            Property(t => t.CoverPicUrl)
                .HasMaxLength(50);

            Property(t => t.Author)
                .HasMaxLength(20);

            Property(t => t.Digest)
                .HasMaxLength(50);

            Property(t => t.Content)
                .IsRequired()
                .HasMaxLength(500);

            Property(t => t.ContentSourceUrl)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.LastUpdateUser)
                .HasMaxLength(20);
        }
    }
}

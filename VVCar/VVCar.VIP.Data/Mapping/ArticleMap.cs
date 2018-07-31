using System.Data.Entity.ModelConfiguration;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    /// <summary>
    /// 图文消息Map
    /// </summary>
    public class ArticleMap : EntityTypeConfiguration<Article>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ArticleMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.LastUpdateUser)
                .HasMaxLength(20);
        }
    }
}

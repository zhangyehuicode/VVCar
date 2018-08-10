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
    /// 公告Map
    /// </summary>
    public class AnnouncementMap : EntityTypeConfiguration<Announcement>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AnnouncementMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.Process)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Remark)
                .HasMaxLength(20);

            Property(t => t.Content)
                .HasMaxLength(200);

            Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.LastUpdateUser)
                .HasMaxLength(20);
        }
    }
}

using System.Data.Entity.ModelConfiguration;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    /// <summary>
    /// 门店标签(客户标签)Map
    /// </summary>
    public class TagMap : EntityTypeConfiguration<Tag>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public TagMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(25);

            Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.LastUpdatedUser)
                .HasMaxLength(20);
        }
    }
}

using System.Data.Entity.ModelConfiguration;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    /// <summary>
    /// 历史消费记录Map
    /// </summary>
    public class ConsumeHistoryMap : EntityTypeConfiguration<ConsumeHistory>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ConsumeHistoryMap()
        {
            this.HasKey(t => t.ID);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PlateNumber)
                .HasMaxLength(10);

            this.Property(t => t.Unit)
                .HasMaxLength(10);

            this.Property(t => t.TradeNo)
                .HasMaxLength(50);

            this.Property(t => t.MobilePhoneNo)
                .HasMaxLength(15);

            this.Property(t => t.Consumption)
                .HasMaxLength(100);

            this.Property(t => t.Remark)
                .HasMaxLength(250);

            this.Property(t => t.DepartmentName)
                .HasMaxLength(20);

            this.Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}

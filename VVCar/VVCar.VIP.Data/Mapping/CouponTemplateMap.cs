using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class CouponTemplateMap : EntityTypeConfiguration<CouponTemplate>
    {
        public CouponTemplateMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.TemplateCode)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Color)
                .IsRequired()
                .HasMaxLength(7);

            Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(18);

            Property(t => t.SubTitle)
                .HasMaxLength(18);

            //Property(t => t.IncludeProducts)
            //    .HasMaxLength(500);

            //Property(t => t.ExcludeProducts)
            //    .HasMaxLength(500);

            Property(t => t.UseDaysOfWeek)
                .HasMaxLength(15);

            Property(t => t.CoverImage)
                .HasMaxLength(100);

            Property(t => t.CoverIntro)
                .HasMaxLength(12);

            Property(t => t.UseInstructions)
                .HasMaxLength(300);

            Property(t => t.IntroDetail)
                .HasMaxLength(5000);

            Property(t => t.MerchantPhoneNo)
                .HasMaxLength(20);

            Property(t => t.ApplyStores)
                .HasMaxLength(500);

            Property(t => t.OperationTips)
                .HasMaxLength(16);

            Property(t => t.CreatedUser)
                .HasMaxLength(20);

            Property(t => t.ApprovedUser)
                .HasMaxLength(20);

            Property(t => t.Remark)
                .HasMaxLength(500);

            // Table Relationships
            HasOptional(t => t.Stock)
                .WithRequired();

            HasMany(t => t.UseTimeList)
                .WithRequired()
                .HasForeignKey(t => t.TemplateID);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    public class RechargeHistoryMap : EntityTypeConfiguration<RechargeHistory>
    {
        public RechargeHistoryMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.TradeNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.OutTradeNo)
                .HasMaxLength(100);

            this.Property(t => t.CardNumber)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.CreatedUser)
                .HasMaxLength(20);

            this.HasRequired(t => t.Card)
                .WithMany()
                .HasForeignKey(t => t.CardID);

            this.HasOptional(t => t.Member)
                .WithMany()
                .HasForeignKey(t => t.MemberID);

            this.HasRequired(t => t.TradeDepartment)
                .WithMany()
                .HasForeignKey(t => t.TradeDepartmentID);
        }
    }
}

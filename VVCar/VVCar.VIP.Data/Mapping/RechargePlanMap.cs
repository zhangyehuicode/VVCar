using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class RechargePlanMap : EntityTypeConfiguration<RechargePlan>
    {
        public RechargePlanMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.CreatedUser)
                .HasMaxLength(20);

            this.Property(t => t.LastUpdateUser)
                .HasMaxLength(20);

            //this.Property(t => t.MatchCardType)
            //    .HasMaxLength(200);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    public class CarInspectionReportMap : EntityTypeConfiguration<CarInspectionReport>
    {
        public CarInspectionReportMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.PlateNumber)
                .IsRequired()
                .HasMaxLength(10);

            Property(t => t.DepartmentName)
                .HasMaxLength(20);

            Property(t => t.OpenID)
                .HasMaxLength(50);

            Property(t => t.Brand)
                .HasMaxLength(20);

            Property(t => t.CarType)
                .HasMaxLength(20);

            Property(t => t.Inspector)
                .HasMaxLength(20);

            Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.LastUpdateUser)
                .HasMaxLength(20);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    public class AppointmentMap : EntityTypeConfiguration<Appointment>
    {
        public AppointmentMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Name)
                .HasMaxLength(20);

            Property(t => t.MobilePhoneNo)
                .HasMaxLength(11);

            Property(t => t.OpenID)
                .HasMaxLength(36);

            Property(t => t.ServiceName)
                .HasMaxLength(20);
        }
    }
}

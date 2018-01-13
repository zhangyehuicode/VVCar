using System.Data.Entity.ModelConfiguration;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    public class UserRoleMap : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            this.Property(t => t.CreatedUser)
                .HasMaxLength(20);

            // Properties
            this.HasRequired(t => t.Role)
                .WithMany()
                .HasForeignKey(t => t.RoleID);
        }
    }
}

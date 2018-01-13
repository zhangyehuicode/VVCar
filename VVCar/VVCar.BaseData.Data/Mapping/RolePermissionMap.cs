using System.Data.Entity.ModelConfiguration;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    public partial class RolePermissionMap : EntityTypeConfiguration<RolePermission>
    {

        public RolePermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            this.Property(t => t.PermissionCode)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.RoleCode)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}

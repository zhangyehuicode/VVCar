using System.Data.Entity.ModelConfiguration;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    public class SysMenuMap : EntityTypeConfiguration<SysMenu>
    {
        public SysMenuMap()
        {
            this.HasKey(p => p.ID);

            this.Property(p => p.SysMenuUrl)
                .HasMaxLength(500);

            this.Property(p => p.Name)
                .HasMaxLength(250);

            this.Property(p => p.SysMenuIcon)
                .HasMaxLength(30);

            this.Property(t => t.Component)
                .HasMaxLength(250);

            this.HasMany<SysMenu>(p => p.Children)
                .WithOptional()
                .HasForeignKey(p => p.ParentID);
        }
    }
}

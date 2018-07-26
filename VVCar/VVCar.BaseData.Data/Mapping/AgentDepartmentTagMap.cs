using System.Data.Entity.ModelConfiguration;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    /// <summary>
    /// 门店标签关联表Map
    /// </summary>
    public class AgentDepartmentTagMap : EntityTypeConfiguration<AgentDepartmentTag>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AgentDepartmentTagMap()
        {
            HasKey(t => t.ID);

            Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}

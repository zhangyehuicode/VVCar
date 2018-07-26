using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    /// <summary>
    /// 门店分类Map
    /// </summary>
    public class AgentDepartmentCategoryMap : EntityTypeConfiguration<AgentDepartmentCategory>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AgentDepartmentCategoryMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);
            Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.LastUpdateUser)
                .HasMaxLength(20);
        }
    }
}

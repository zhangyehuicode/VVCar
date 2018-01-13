using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace YEF.Data
{
    public class DataUpdateRecordMap : EntityTypeConfiguration<DataUpdateRecord>
    {
        public DataUpdateRecordMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.EntityName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EntityFullName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.CreatedUser)
                .HasMaxLength(20);
        }
    }
}

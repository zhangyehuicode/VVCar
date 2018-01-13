using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Data
{
    public class MigrationsConfiguration : DbMigrationsConfiguration<EfDbContext>
    {
        static MigrationsConfiguration()
        {
        }

        /// <summary>
        /// 初始化一个<see cref="MigrationsConfiguration"/>类型的新实例
        /// </summary>
        public MigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}

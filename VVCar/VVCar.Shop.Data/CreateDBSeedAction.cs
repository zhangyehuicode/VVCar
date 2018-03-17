using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Data.Initializer;

namespace VVCar.Shop.Data
{
    /// <summary>
    /// 创建数据库初始化设置种子数据
    /// </summary>
    public class CreateDBSeedAction : ICreateDBSeedAction
    {
        const string _systemUserName = "system";

        /// <summary>
        /// 操作顺序，数值越小越先执行
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public int Order
        {
            get { return 0; }
        }

        /// <summary>
        /// 向上下文添加种子数据
        /// </summary>
        /// <param name="context"></param>
        public void Seed(DbContext context)
        {

        }
    }
}

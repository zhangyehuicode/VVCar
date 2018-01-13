using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Data.Initializer
{
    /// <summary>
    /// 创建数据库初始化设置种子数据
    /// </summary>
    public interface ICreateDBSeedAction
    {
        /// <summary>
        /// 操作顺序，数值越小越先执行
        /// </summary>
        int Order { get; }

        /// <summary>
        /// 向上下文添加种子数据
        /// </summary>
        /// <param name="context">数据上下文</param>
        void Seed(DbContext context);
    }
}

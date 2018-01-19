using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Domain;
using YEF.Core.Data;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Domain.Services
{
    /// <summary>
    /// 编码规则 领域服务接口
    /// </summary>
    public partial interface IMakeCodeRuleService : IDomainService<IRepository<MakeCodeRule>, MakeCodeRule, Guid>
    {
        /// <summary>
        /// 获取编号，不保存数据库
        /// </summary>
        /// <param name="codeType">类型</param>
        /// <returns></returns>
        string GetCode(string codeType);

        /// <summary>
        /// 生成编号，保存数据库
        /// </summary>
        /// <param name="codeType">类型</param>
        /// <returns></returns>
        string GenerateCode(string codeType);

        /// <summary>
        /// 按基准时间生成编号
        /// </summary>
        /// <param name="codeType">类型</param>
        /// <param name="dateTime">基准时间</param>
        /// <returns></returns>
        string GenerateCode(string codeType, DateTime dateTime);

        /// <summary>
        /// 重置编号当前值为0
        /// </summary>
        /// <param name="codeType">类型</param>
        void ResetCode(string codeType);
    }
}

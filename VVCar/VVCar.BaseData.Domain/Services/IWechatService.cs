using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.BaseData.Domain.Services
{
    /// <summary>
    /// 微信领域服务接口
    /// </summary>
    public partial interface IWechatService : IDomainService<IRepository<Wechat>, Wechat, Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        string Index(WechatFilter filter);
    }
}

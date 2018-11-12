using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.BaseData.Services.DomainServices
{
    /// <summary>
    /// 微信服务
    /// </summary>
    public partial class WechatService : DomainServiceBase<IRepository<Wechat>, Wechat, Guid>, IWechatService
    {
        public WechatService()
        {
        }


    }
}

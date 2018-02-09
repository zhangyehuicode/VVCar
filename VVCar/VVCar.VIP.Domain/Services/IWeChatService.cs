using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using YEF.Core;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 微信服务
    /// </summary>
    public interface IWeChatService : IDependency
    {
        /// <summary>
        /// 发送微信通知
        /// </summary>
        /// <param name="message"></param>
        void SendWeChatNotify(WeChatTemplateMessageDto message);

        /// <summary>
        /// 异步发送微信通知
        /// </summary>
        /// <param name="message"></param>
        void SendWeChatNotifyAsync(WeChatTemplateMessageDto message);
    }
}

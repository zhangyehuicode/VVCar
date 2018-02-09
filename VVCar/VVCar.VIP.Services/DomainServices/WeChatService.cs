using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Utility;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 微信服务
    /// </summary>
    public class WeChatService : IWeChatService
    {
        /// <summary>
        /// 发送微信通知
        /// </summary>
        /// <param name="message"></param>
        public void SendWeChatNotify(WeChatTemplateMessageDto message)
        {
            if (string.IsNullOrEmpty(AppContext.Settings.WeChatIntegrationService))
            {
                AppContext.Logger.Error("微信集成服务地址未配置，跳过微信通知");
                return;
            }
            var url = string.Format("{0}/Notify/SendTemplateMessage?companyCode={1}",
                AppContext.Settings.WeChatIntegrationService,
                AppContext.CurrentSession.CompanyCode);
            try
            {
                var client = new System.Net.Http.HttpClient();
                var json = JsonHelper.Serialize(message);
                var content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(url, content).Result;
                var responseData = response.Content.ReadAsStringAsync().Result;
                AppContext.Logger.Debug("发送微信通知 结果[{0}]", responseData);
            }
            catch (Exception ex)
            {
                AppContext.Logger.Error("发送微信通知 出现错误.", ex);
            }
        }

        /// <summary>
        /// 异步发送微信通知
        /// </summary>
        /// <param name="message"></param>
        public void SendWeChatNotifyAsync(WeChatTemplateMessageDto message)
        {
            Task.Run(async () =>
            {
                await Task.Delay(5000);
                SendWeChatNotify(message);
            });
        }
    }
}

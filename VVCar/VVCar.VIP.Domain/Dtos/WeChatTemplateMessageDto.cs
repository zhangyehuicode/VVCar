using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 微信模板消息
    /// </summary>
    public class WeChatTemplateMessageDto
    {
        /// <summary>
        /// 消息数据
        /// </summary>
        public class MessageData
        {
            /// <summary>
            /// 消息数据
            /// </summary>
            /// <param name="dataValue"></param>
            public MessageData(string dataValue) : this(dataValue, null)
            {
            }

            /// <summary>
            /// 消息数据
            /// </summary>
            /// <param name="dataValue"></param>
            /// <param name="dataColor"></param>
            public MessageData(string dataValue, string dataColor)
            {
                value = dataValue;
                color = dataColor;
            }

            /// <summary>
            /// 字段值
            /// </summary>
            public string value { get; set; }

            /// <summary>
            /// 颜色
            /// </summary>
            public string color { get; set; }
        }

        /// <summary>
        /// 发送到的OpenId
        /// </summary>
        public string touser { get; set; }

        /// <summary>
        /// 微信消息模板ID
        /// </summary>
        public string template_id { get; set; }

        /// <summary>
        /// 点击模板消息后跳转到的URL
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 消息数据
        /// </summary>
        public dynamic data { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 微信公众平台信息
    /// </summary>
    public partial class Wechat : EntityBase
    {
        /// <summary>
        /// 微信服务器Token
        /// </summary>
        [Display(Name = "微信服务器Token")]
        public string Token { get; set; }

        /// <summary>
        /// 加密字符串
        /// </summary>
        [Display(Name = "加密字符串")]
        public string EncodingAESKey { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid? CreatedUserID { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [Display(Name = "创建人名称")]
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime? CreatedDate { get; set; }
    }
}

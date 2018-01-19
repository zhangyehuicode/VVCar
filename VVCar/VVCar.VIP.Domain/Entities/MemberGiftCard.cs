using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 会员礼品卡
    /// </summary>
    public class MemberGiftCard : NormalEntityBase
    {
        /// <summary>
        /// 卡片主题ID
        /// </summary>
        [Display(Name = "卡片主题ID")]
        public Guid? MemberCardThemeID { get; set; }

        /// <summary>
        /// 卡片主题
        /// </summary>
        [Display(Name = "卡片主题")]
        public virtual MemberCardTheme MemberCardTheme { get; set; }

        /// <summary>
        /// OpenID
        /// </summary>
        [Display(Name = "OpenID")]
        public string OpenID { get; set; }

        /// <summary>
        /// 礼品卡ID
        /// </summary>
        [Display(Name = " 礼品卡ID")]
        public Guid CardID { get; set; }

        /// <summary>
        /// 礼品卡
        /// </summary>
        [Display(Name = "礼品卡")]
        public virtual MemberCard Card { get; set; }

        /// <summary>
        /// 支付订单号
        /// </summary>
        [Display(Name = "支付订单号")]
        public string OrderNo { get; set; }

        /// <summary>
        /// 储值方案编号
        /// </summary>
        [Display(Name = "储值方案编号")]
        public string RechargePlanCode { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public Guid? CreatedUserID { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [Display(Name = "创建人")]
        public String CreatedUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [Display(Name = "最后修改人ID")]
        public Guid? LastUpdateUserID { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [Display(Name = "最后修改人")]
        public String LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Display(Name = "最后修改时间")]
        public DateTime? LastUpdateDate { get; set; }
    }
}

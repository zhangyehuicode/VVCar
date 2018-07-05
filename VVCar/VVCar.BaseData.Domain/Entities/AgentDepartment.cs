using System;
using System.ComponentModel.DataAnnotations;
using VVCar.BaseData.Domain.Enums;
using YEF.Core.Data;
using YEF.Core.Enums;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 代理商门店
    /// </summary>
    public class AgentDpartment : EntityBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Display(Name = "用户ID")]
        public Guid? UserID { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 是否总部
        /// </summary>
        [Display(Name = "是否总部")]
        public bool IsHQ { get; set; }

        /// <summary>
        /// 法人(负责人)
        /// </summary>
        [Display(Name = "法人(负责人)")]
        public string LegalPerson { get; set; }

        /// <summary>
        /// 法人身份证编号
        /// </summary>
        [Display(Name = "法人身份证编号")]
        public string IDNumber { get; set; }

        /// <summary>
        /// 注册邮箱
        /// </summary>
        [Display(Name = "注册邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// 微信公众号登录密码
        /// </summary>
        [Display(Name = "微信公众号登录密码")]
        public string WeChatOAPassword { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Display(Name = "联系电话")]
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 营业执照 图片地址
        /// </summary>
        [Display(Name = "营业执照 图片地址")]
        public string BusinessLicenseImgUrl { get; set; }

        /// <summary>
        /// 法人身份证正面 图片地址
        /// </summary>
        [Display(Name = "法人身份证正面 图片地址")]
        public string LegalPersonIDCardFrontImgUrl { get; set; }

        /// <summary>
        /// 法人身份证反面 图片地址
        /// </summary>
        [Display(Name = "法人身份证反面 图片地址")]
        public string LegalPersonIDCardBehindImgUrl { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        [Display(Name = "公司地址")]
        public string CompanyAddress { get; set; }

        /// <summary>
        /// 微信公众号AppID
        /// </summary>
        [Display(Name = "微信公众号AppID")]
        public string WeChatAppID { get; set; }

        /// <summary>
        /// 微信公众号AppSecret
        /// </summary>
        [Display(Name = "微信公众号AppSecret")]
        public string WeChatAppSecret { get; set; }

        /// <summary>
        /// 微信商户号（微信商户平台）
        /// </summary>
        [Display(Name = "微信商户号（微信商户平台）")]
        public string WeChatMchID { get; set; }

        /// <summary>
        /// 微信商户Key（微信商户平台 支付密钥）
        /// </summary>
        [Display(Name = "微信商户Key（微信商户平台 支付密钥）")]
        public string WeChatMchKey { get; set; }

        /// <summary>
        /// 微信商户操作密码
        /// </summary>
        [Display(Name = "微信商户操作密码")]
        public string MeChatMchPassword { get; set; }

        /// <summary>
        /// 开户行
        /// </summary>
        [Display(Name = "开户行")]
        public string Bank { get; set; }

        /// <summary>
        /// 开户行账号
        /// </summary>
        [Display(Name = "开户行账号")]
        public string BankCard { get; set; }

        /// <summary>
        /// 商户门店审核状态
        /// </summary>
        [Display(Name = "商户门店审核状态")]
        public EAgentDepartmentApproveStatus ApproveStatus { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        [Display(Name = "数据来源")]
        public EAgentDepartmentSource DataSource { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid CreatedUserID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [Display(Name = "最后修改人ID")]
        public Guid? LastUpdatedUserID { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [Display(Name = "最后修改人")]
        public string LastUpdatedUser { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Display(Name = "最后修改时间")]
        public DateTime? LastUpdatedDate { get; set; }

        /// <summary>
        /// 代理商
        /// </summary>
        public virtual Merchant Merchant { get; set; }
    }
}

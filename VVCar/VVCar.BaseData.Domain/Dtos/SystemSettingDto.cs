using System;
using System.ComponentModel.DataAnnotations;
using VVCar.BaseData.Domain.Enums;

namespace VVCar.BaseData.Domain.Dtos
{
    /// <summary>
    /// 系统设置Dto
    /// </summary>
    public class SystemSettingDto
    {
        /// <summary>
        /// 系统设置ID
        /// </summary>
        [Display(Name = "系统设置ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 商户 Code
        /// </summary>
        [Display(Name = "商户 Code")]
        public String MerchantCode { get; set; }

        /// <summary>
        /// 商户 Name
        /// </summary>
        [Display(Name = "商户 Name")]
        public String MerchantName { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [Display(Name = "序号")]
        public int Index { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        [Display(Name = "参数类型")]
        public ESystemSettingType Type { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        [Display(Name = "参数名称")]
        public string Name { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = "标题")]
        public string Caption { get; set; }

        /// <summary>
        /// 模板标题
        /// </summary>
        [Display(Name = "模板标题")]
        public string TemplateName { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [Display(Name = "默认值")]
        public string DefaultValue { get; set; }

        /// <summary>
        /// 设置值
        /// </summary>
        [Display(Name = "设置值")]
        public string SettingValue { get; set; }

        /// <summary>
        /// 是否可见
        /// </summary>
        [Display(Name = "是否可见")]
        public bool IsVisible { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Display(Name = "是否可用")]
        public bool IsAvailable { get; set; }
    }
}

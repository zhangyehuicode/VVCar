using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using YEF.Core.Data;
using VVCar.BaseData.Domain.Enums;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 系统设置
    /// </summary>
    public partial class SystemSetting : EntityBase
    {
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
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
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
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改日期
        /// </summary>
        [Display(Name = "最后修改日期")]
        public DateTime? LastUpdateDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace YEF.Core.Data
{
    /// <summary>
    /// 数据记录
    /// </summary>
    public class DataUpdateRecord : EntityBase<int>
    {
        /// <summary>
        /// 实体名称
        /// </summary>
        [Display(Name = "实体名称")]
        public string EntityName { get; set; }

        /// <summary>
        /// 实体强类型名称
        /// </summary>
        [Display(Name = "实体强类型名称")]
        public string EntityFullName { get; set; }

        /// <summary>
        /// 实体ID
        /// </summary>
        [Display(Name = "实体ID")]
        public Guid DataID { get; set; }

        /// <summary>
        /// 更新类型
        /// </summary>
        [Display(Name = "更新类型")]
        public UpdateType UpdateType { get; set; }

        /// <summary>
        /// 目标门店ID
        /// </summary>
        [Display(Name = "目标门店")]
        public Guid? TargetDepartmentID { get; set; }

        /// <summary>
        /// 数据方向
        /// </summary>
        [Display(Name = "数据方向")]
        public DataDirection Direction { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public RecordStatus Status { get; set; }

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
    }

    /// <summary>
    /// 更新类型
    /// </summary>
    public enum UpdateType
    {
        /// <summary>
        /// 插入
        /// </summary>
        Insert = 10,

        /// <summary>
        /// 更新
        /// </summary>
        Update = 20,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 30
    }

    /// <summary>
    /// 记录状态
    /// </summary>
    public enum RecordStatus
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,

        /// <summary>
        /// 同步中
        /// </summary>
        Syncing = 1,

        /// <summary>
        /// 完成
        /// </summary>
        Finish = -1,
    }
}

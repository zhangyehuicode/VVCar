using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 畅销/滞销产品通知配置
    /// </summary>
    public class UnsaleProductSetting : EntityBase
    {
        public UnsaleProductSetting()
        {
            unsaleProductSettingItemList = new List<UnsaleProductSettingItem>();
        }

        /// <summary>
        /// 配置编码
        /// </summary>
        [Display(Name = "配置编码")]
        public string Code { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        [Display(Name = "配置名称")]
        public string Name { get; set; }

        /// <summary>
        /// 滞销数量上限值,低于即视为滞销产品
        /// </summary>
        [Display(Name = "滞销数量上限值,低于即视为滞销产品")]
        public int UnsaleQuantity { get; set; }

        /// <summary>
        /// 畅销数量下限值,高于则视为畅销产品
        /// </summary>
        [Display(Name = "畅销数量下限值,高于则视为畅销产品")]
        public int SaleWellQuantity { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid? CreatedUserID { get; set; }

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
        public Guid? LastUpdateUserID { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [Display(Name = "最后修改人")]
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Display(Name = "最后修改时间")]
        public DateTime? LastUpdateDate { get; set; }


        /// <summary>
        /// 畅销/滞销产品子项
        /// </summary>
        public ICollection<UnsaleProductSettingItem> unsaleProductSettingItemList { get; set; }
    }
}

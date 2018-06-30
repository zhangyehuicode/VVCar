using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 车比特产品类别树形Dto
    /// </summary>
    public class CarBitCoinProductCategoryTreeDto : TreeNodeModel<CarBitCoinProductCategoryTreeDto>
    {
        /// <summary>
        /// 文本
        /// </summary>
        [Display(Name = "文本")]
        public override string Text
        {
            get { return Name; }
            set { Name = value; }
        }

        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        public Guid? ID { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        [Display(Name = "父级ID")]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 类别编号
        /// </summary>
        [Display(Name = "类别编号")]
        public string Code { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        [Display(Name = "类别名称")]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        public int Index { get; set; }

        /// <summary>
        /// 是否会员产品
        /// </summary>
        [Display(Name = "是否会员产品")]
        public bool IsForMember { get; set; }

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
        public Guid? LastUpdateUserID { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [Display(Name = "最后修改人")]
        public string LastUpdateUser { get; set; }
    }
}

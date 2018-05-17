using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class ShoppingCart : NormalEntityBase
    {
        public ShoppingCart()
        {
            ShoppingCartItemList = new List<ShoppingCartItem>();
        }

        /// <summary>
        /// 会员ID
        /// </summary>
        [Display(Name = "会员ID")]
        public Guid? MemberID { get; set; }

        /// <summary>
        /// OpenID
        /// </summary>
        [Display(Name = "OpenID")]
        public string OpenID { get; set; }

        /// <summary>
        /// 总额
        /// </summary>
        [Display(Name = "总额")]
        public decimal Money { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Display(Name = "最后修改时间")]
        public DateTime? LastUpdatedDate { get; set; }

        /// <summary>
        /// 购物车子项
        /// </summary>
        [Display(Name = "购物车子项")]
        public ICollection<ShoppingCartItem> ShoppingCartItemList { get; set; }
    }
}

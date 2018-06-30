using System.ComponentModel.DataAnnotations;

namespace VVCar.Shop.Domain.Enums
{
    /// <summary>
    /// 车比特产品类型
    /// </summary>
    public enum ECarBitCoinProductType
    {
        /// <summary>
        /// 引擎
        /// </summary>
        [Display(Name = "引擎")]
        Engine = 0,

        /// <summary>
        /// 商品
        /// </summary>
        [Display(Name = "商品")]
        Goods = 1,
    }
}

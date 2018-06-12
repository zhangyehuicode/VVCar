using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 优惠券完整信息
    /// </summary>
    public class CouponFullInfoDto
    {
        /// <summary>
        /// 优惠券ID
        /// </summary>
        public Guid? CouponID { get; set; }

        /// <summary>
        ///  优惠券编号
        /// </summary>
        public string CouponCode { get; set; }

        /// <summary>
        ///  优惠券模板ID
        /// </summary>
        public Guid TemplateID { get; set; }

        /// <summary>
        /// 性质
        /// </summary>
        public ENature Nature { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 截止日期说明
        /// </summary>
        public string EffectiveDateDesc
        {
            get
            {
                return EffectiveDate.ToString("yyyy.MM.dd");
            }
        }

        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        /// 截止日期说明
        /// </summary>
        public string ExpiredDateDesc
        {
            get
            {
                return ExpiredDate.ToString("yyyy.MM.dd");
            }
        }

        /// <summary>
        ///  使用状态
        /// </summary>
        public ECouponStatus Status { get; set; }

        /// <summary>
        /// 领取人OpenID
        /// </summary>
        public string OwnerOpenID { get; set; }

        /// <summary>
        /// 领取人昵称
        /// </summary>
        public string OwnerNickName { get; set; }

        /// <summary>
        /// 领取人电话
        /// </summary>
        public string OwnerPhoneNo { get; set; }

        /// <summary>
        ///  类型
        /// </summary>
        public ECouponType CouponType { get; set; }

        /// <summary>
        ///  颜色
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        ///  券面值，抵用券时为抵用金额，代金券时为减免金额
        /// </summary>
        public decimal CouponValue { get; set; }

        /// <summary>
        /// 券面值单位
        /// </summary>
        public string CouponValueUnit
        {
            get
            {
                return CouponType == ECouponType.Discount ? "折" : "元";
            }
        }

        /// <summary>
        ///  标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///  副标题
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        ///  是否限制最低消费
        /// </summary>
        public bool IsMinConsumeLimit { get; set; }

        /// <summary>
        ///  最低消费金额，单位(元)
        /// </summary>
        public decimal MinConsume { get; set; }

        /// <summary>
        ///  不与其他优惠共享
        /// </summary>
        public bool IsExclusive { get; set; }

        /// <summary>
        /// 使用说明
        /// </summary>
        public string UseCondition
        {
            get
            {
                var condition = IsMinConsumeLimit ? string.Format("满 {0} 元可用 ", MinConsume.ToString("0.##")) : string.Empty;
                if (IsExclusive)
                    condition = condition + " 不与其他优惠共享";

                return condition;
            }
        }

        /// <summary>
        ///  适用商品编码，以,分隔
        /// </summary>
        public string IncludeProducts { get; set; }

        /// <summary>
        ///  不适用商品编码，以,分隔
        /// </summary>
        public string ExcludeProducts { get; set; }

        /// <summary>
        ///  是否全时段
        /// </summary>
        public bool IsUseAllTime { get; set; }

        /// <summary>
        ///  可用的日期
        /// </summary>
        public string UseDaysOfWeek { get; set; }

        /// <summary>
        ///  封面图片
        /// </summary>
        public string CoverImage { get; set; }

        /// <summary>
        ///  封面简介
        /// </summary>
        public string CoverIntro { get; set; }

        /// <summary>
        ///  使用须知
        /// </summary>
        public string UseInstructions { get; set; }

        /// <summary>
        ///  图文介绍
        /// </summary>
        public string IntroDetail { get; set; }

        /// <summary>
        ///  商户电话
        /// </summary>
        public string MerchantPhoneNo { get; set; }

        /// <summary>
        ///  商户服务
        /// </summary>
        public EMerchantService MerchantService { get; set; }

        /// <summary>
        /// 商户服务说明
        /// </summary>
        public string MerchantServiceDesc
        {
            get
            {
                var builder = new StringBuilder();
                if ((MerchantService & EMerchantService.FreeWifi) != 0)
                {
                    builder.Append("免费WIFI ");
                }
                if ((MerchantService & EMerchantService.FreePark) != 0)
                {
                    builder.Append("免费停车 ");
                }
                if ((MerchantService & EMerchantService.AllowPets) != 0)
                {
                    builder.Append("可带宠物 ");
                }
                if ((MerchantService & EMerchantService.TakeOut) != 0)
                {
                    builder.Append("外卖 ");
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// 用户可分享链接
        /// </summary>
        public bool CanShareByPeople { get; set; }

        /// <summary>
        /// 用户可以赠送优惠券
        /// </summary>
        public bool CanGiveToPeople { get; set; }

        /// <summary>
        ///  核销方式
        /// </summary>
        public EVerificationMode VerificationMode { get; set; }

        /// <summary>
        /// 全部门店适用
        /// </summary>
        public bool IsApplyAllStore { get; set; }

        /// <summary>
        ///  适用门店编码，以,分隔
        /// </summary>
        public string ApplyStores { get; set; }

        /// <summary>
        ///  操作提示
        /// </summary>
        public string OperationTips { get; set; }

        /// <summary>
        /// 可用时段
        /// </summary>
        public IEnumerable<CouponTemplateUseTime> CouponTemplateUseTimes { get; set; }

        /// <summary>
        ///  库存 / 发行量
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 已领数量
        /// </summary>
        public int UsedStock { get; set; }

        /// <summary>
        /// 剩余库存
        /// </summary>
        public int FreeStock { get { return Stock - UsedStock; } }

        /// <summary>
        /// 领券数量限制
        /// </summary>
        public int CollarQuantityLimit { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public string UseTimeDesc
        {
            get
            {
                if (IsUseAllTime)
                {
                    return "全时段";
                }
                var builder = new StringBuilder();
                if (!string.IsNullOrEmpty(UseDaysOfWeek))
                {
                    var daysofweek = UseDaysOfWeek.Split(',');
                    for (var i = 0; i < daysofweek.Length; i++)
                    {
                        switch (daysofweek[i])
                        {
                            case "0":
                                builder.Append("周日 ");
                                break;
                            case "1":
                                builder.Append("周一 ");
                                break;
                            case "2":
                                builder.Append("周二 ");
                                break;
                            case "3":
                                builder.Append("周三 ");
                                break;
                            case "4":
                                builder.Append("周四 ");
                                break;
                            case "5":
                                builder.Append("周五 ");
                                break;
                            case "6":
                                builder.Append("周六 ");
                                break;
                        }
                    }
                }
                if (CouponTemplateUseTimes != null)
                {
                    builder.AppendLine();
                    foreach (var useTime in CouponTemplateUseTimes)
                    {
                        if (useTime.Type == EUseTimeType.Use)
                        {
                            builder.AppendLine(useTime.BeginTime + " ~ " + useTime.EndTime);
                        }
                    }
                }
                return builder.ToString();
            }
        }
    }
}

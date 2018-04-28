using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;
using YEF.Core;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 优惠券概要 DTO, 用于微信端用户券列表
    /// </summary>
    public class CouponBaseInfoDto
    {
        /// <summary>
        /// 券ID
        /// </summary>
        public Guid CouponID { get; set; }

        /// <summary>
        ///  优惠券编号
        /// </summary>
        public string CouponCode { get; set; }

        /// <summary>
        /// 券模板ID
        /// </summary>
        public Guid TemplateID { get; set; }

        /// <summary>
        /// 性质
        /// </summary>
        public ENature Nature { get; set; }

        /// <summary>
        ///  封面图片
        /// </summary>
        public string CoverImage { get; set; }

        /// <summary>
        ///  使用状态
        /// </summary>
        public ECouponStatus Status { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 生效日期星期
        /// </summary>
        public string EffectiveDateWeek
        {
            get
            {
                if (DateTime.Now.Year >= EffectiveDate.Year && DateTime.Now.Month >= EffectiveDate.Month)
                {
                    return "当前月";
                }
                return EffectiveDate.ToString("MMMM");
            }
        }

        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime? ExpiredDate { get; set; }

        /// <summary>
        /// 是否固定有效期
        /// </summary>
        public bool IsFiexedEffectPeriod { get; set; }

        /// <summary>
        ///  领取后多少天生效
        /// </summary>
        public int? EffectiveDaysAfterReceived { get; set; }

        /// <summary>
        ///  有效天数
        /// </summary>
        public int? EffectiveDays { get; set; }

        /// <summary>
        /// 过期日期
        /// </summary>
        public string ExpiredDateDesc
        {
            get
            {
                if (!IsFiexedEffectPeriod && EffectiveDaysAfterReceived.HasValue && EffectiveDays.HasValue)
                {
                    return $"领取后{EffectiveDaysAfterReceived.Value}天生效，{EffectiveDays.Value}天有效";
                }
                else if (ExpiredDate.HasValue)
                {
                    return ExpiredDate.Value.ToString("yyyy.MM.dd");
                }
                return string.Empty;
                //return ExpiredDate?.ToString("yyyy.MM.dd") ?? string.Empty;
            }
        }

        /// <summary>
        /// 是否临近过期
        /// </summary>
        public bool IsNearExpiration
        {
            get
            {
                return ExpiredDate.HasValue && ExpiredDate.Value.AddDays(-7) < DateTime.Now;
            }
        }

        /// <summary>
        /// 券标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///  类型
        /// </summary>
        public ECouponType CouponType { get; set; }

        /// <summary>
        ///  类型说明
        /// </summary>
        public string CouponTypeDesc { get { return CouponType.GetDescription(); } }

        /// <summary>
        ///  券面值，抵用券时为抵用金额，代金券时为减免金额，折扣券时为折扣比例
        /// </summary>
        public decimal CouponValue { get; set; }

        /// <summary>
        /// 券面值说明
        /// </summary>
        public string CouponValueDesc { get { return CouponValue.ToString("0.#"); } }

        /// <summary>
        /// 券面值单位
        /// </summary>
        public string CouponValueUnit { get { return CouponType == ECouponType.Discount ? "折" : "元"; } }

        /// <summary>
        ///  颜色
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 是否限制最低消费
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
        /// 是否已领取
        /// </summary>
        public bool IsReceived { get; set; }

        /// <summary>
        ///  库存 / 发行量
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 已领数量
        /// </summary>
        public int UsedStock { get; set; }

        /// <summary>
        /// 领取量（已抢：百分比）
        /// </summary>  ///String.Format("{0:F}",dbdata)
        public string ReceiveAmount
        {
            get
            {
                return Stock != 0 ? $"{Math.Round((UsedStock / (decimal)Stock) * 100, 0)}%" : "100%";
            }
        }

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
        /// 兑换方式
        /// </summary>
        public EExchangeType ExchangeType { get; set; }

        /// <summary>
        /// 兑换积分
        /// </summary>
        public int ExchangePoint { get; set; }

        /// <summary>
        /// 兑换结束日期
        /// </summary>
        public DateTime ExchangeFinishDate { get; set; }

        /// <summary>
        /// 兑换结束日期 描述
        /// </summary>
        public string ExchangeFinishDateDesc { get { return ExchangeFinishDate.ToString("yyyy.MM.dd"); } }

        /// <summary>
        /// 会员积分
        /// </summary>
        public int MemberPoint { get; set; }

        /// <summary>
        /// 消费总额
        /// </summary>
        public decimal TotalConsume { get; set; }

        /// <summary>
        /// 领券中心兑换设置ID
        /// </summary>
        public Guid PointExchangeCouponID { get; set; }

        /// <summary>
        /// 是否优先抵扣
        /// </summary>
        public bool IsDeductionFirst { get; set; }
    }
}

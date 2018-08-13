using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.Shop.Domain.Services;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Api
{
    /// <summary>
    /// 后台任务接口
    /// </summary>
    [ApiAuthorize(NeedLogin = false)]
    [ApiAuthorize(NeedCompanyCode = false)]
    [RoutePrefix("api/BackgroundTask")]
    public class BackgroundTaskController : BaseApiController
    {
        /// <summary>
        /// 后台任务接口
        /// </summary>
        public BackgroundTaskController()
        {
        }

        #region properties
        /// <summary>
        /// 优惠券 领域服务
        /// </summary>
        ICouponService CouponService
        {
            get { return ServiceLocator.Instance.GetService<ICouponService>(); }
        }

        /// <summary>
        /// 卡券推送 领域服务
        /// </summary>
        ICouponPushService CouponPushService
        {
            get
            {
                return ServiceLocator.Instance.GetService<ICouponPushService>();
            }
        }

        IOrderService OrderService
        {
            get
            {
                return ServiceLocator.Instance.GetService<IOrderService>();
            }
        }

        IServicePeriodService ServicePeriodService
        {
            get
            {
                return ServiceLocator.Instance.GetService<IServicePeriodService>();
            }
        }

        ICarBitCoinDistributionService CarBitCoinDistributionService
        {
            get
            {
                return ServiceLocator.Instance.GetService<ICarBitCoinDistributionService>();
            }
        }

        IGamePushService GamePushService
        {
            get
            {
                return ServiceLocator.Instance.GetService<IGamePushService>();
            }
        }

        ///// <summary>
        ///// 微信粉丝 领域服务
        ///// </summary>
        //IWeChatFansService WeChatFansService
        //{
        //    get { return ServiceLocator.Instance.GetService<IWeChatFansService>(); }
        //}

        ///// <summary>
        ///// 卡券推送 领域服务
        ///// </summary>
        //ICouponPushService CouponPushService
        //{
        //    get { return ServiceLocator.Instance.GetService<ICouponPushService>(); }
        //}
        #endregion

        /// <summary>
        /// 卡券过期提醒
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("CouponReminder"), AllowAnonymous]
        public JsonActionResult<bool> CouponReminder()
        {
            return SafeExecute(() =>
            {
                CouponService.SendCouponExpiredNotify();
                return true;
            });
        }

        /// <summary>
        /// 卡券推送
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("PushCoupon"), AllowAnonymous]
        public JsonActionResult<bool> CouponPush()
        {
            return SafeExecute(() =>
            {
                return CouponPushService.CouponPushTask();
            });
        }

        /// <summary>
        /// 回执发送
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("RevisitTips"), AllowAnonymous]
        public JsonActionResult<bool> RevisitTips()
        {
            return SafeExecute(() =>
            {
                return OrderService.RevisitTipsTask();
            });
        }

        /// <summary>
        /// 游戏推送
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GamePush"), AllowAnonymous]
        public JsonActionResult<bool> GamePush()
        {
            return SafeExecute(() =>
            {
                return GamePushService.GamePushTask();
            });
        }

        /// <summary>
        /// 服务周期提醒
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("ServicePeriodReminder"), AllowAnonymous]
        public JsonActionResult<bool> ServicePeriodReminder()
        {
            return SafeExecute(() =>
            {
                return ServicePeriodService.ServicePeriodReminder();
            });
        }

        /// <summary>
        /// 分配车比特
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("DistributionCarBitCoin"), AllowAnonymous]
        public JsonActionResult<bool> DistributionCarBitCoin()
        {
            return SafeExecute(() =>
            {
                return CarBitCoinDistributionService.DistributionCarBitCoin(null);
            });
        }

        ///// <summary>
        ///// 同步微信粉丝
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet, Route("SyncWeChatFans"), AllowAnonymous]
        //public JsonActionResult<bool> SyncWeChatFans()
        //{
        //    return SafeExecute(() =>
        //    {
        //        WeChatFansService.SyncWeiXinMember();
        //        return true;
        //    });
        //}

        ///// <summary>
        ///// 推送优惠券
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet, Route("PushCoupon"), AllowAnonymous]
        //public JsonActionResult<bool> PushCoupon()
        //{
        //    return SafeExecute(() =>
        //    {
        //        CouponPushService.StartPushCoupon(DateTime.Now);
        //        return true;
        //    });
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Enums;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    public class PickUpOrderService : DomainServiceBase<IRepository<PickUpOrder>, PickUpOrder, Guid>, IPickUpOrderService
    {
        public PickUpOrderService()
        {
        }

        #region properties

        IRepository<MakeCodeRule> MakeCodeRuleRepo { get => UnitOfWork.GetRepository<IRepository<MakeCodeRule>>(); }

        IRepository<PickUpOrderPaymentDetails> PickUpOrderPaymentDetailsRepo { get => UnitOfWork.GetRepository<IRepository<PickUpOrderPaymentDetails>>(); }

        IPickUpOrderPaymentDetailsService PickUpOrderPaymentDetailsService { get => ServiceLocator.Instance.GetService<IPickUpOrderPaymentDetailsService>(); }

        IRepository<Coupon> CouponRepo { get => UnitOfWork.GetRepository<IRepository<Coupon>>(); }

        ICouponService CouponService { get => ServiceLocator.Instance.GetService<ICouponService>(); }

        IRepository<PickUpOrderItem> PickUpOrderItemRepo { get => UnitOfWork.GetRepository<IRepository<PickUpOrderItem>>(); }

        #endregion

        public string GetTradeNo()
        {
            var newTradeNo = string.Empty;
            var existNo = false;
            var makeCodeRuleService = ServiceLocator.Instance.GetService<IMakeCodeRuleService>();
            var entity = Repository.GetQueryable(false).OrderByDescending(t => t.CreatedDate).FirstOrDefault();
            if (entity != null && entity.CreatedDate.Date != DateTime.Now.Date)
            {
                var rule = MakeCodeRuleRepo.GetQueryable().Where(t => t.Code == "PickUpOrder" && t.IsAvailable).FirstOrDefault();
                if (rule != null)
                {
                    rule.CurrentValue = 0;
                    MakeCodeRuleRepo.Update(rule);
                }
            }
            do
            {
                newTradeNo = makeCodeRuleService.GenerateCode("PickUpOrder", DateTime.Now);
                existNo = Repository.Exists(t => t.Code == newTradeNo);
            } while (existNo);
            return newTradeNo;
        }

        public override PickUpOrder Add(PickUpOrder entity)
        {
            if (entity == null || entity.PickUpOrderItemList == null || entity.PickUpOrderItemList.Count < 1)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;

            if (string.IsNullOrEmpty(entity.Code))
                entity.Code = GetTradeNo();

            entity.PickUpOrderItemList.ForEach(t =>
            {
                t.ID = Util.NewID();
                t.PickUpOrderID = entity.ID;
                t.MerchantID = entity.MerchantID;
                t.Money = t.Quantity * t.PriceSale;
                t.Discount = 1;
            });
            RecountMoney(entity);
            return base.Add(entity);
        }

        public void RecountMoney(PickUpOrder entity)
        {
            if (entity == null)
                return;
            if (entity.PickUpOrderItemList == null || entity.PickUpOrderItemList.Count < 1)
                entity.Money = 0;
            else
            {
                decimal totalMoney = 0;
                entity.PickUpOrderItemList.ForEach(t =>
                {
                    //t.Money = t.Quantity * t.PriceSale;
                    totalMoney += t.Quantity * t.PriceSale;
                });
                entity.Money = totalMoney;
            }

            decimal paymoney = 0;
            var paymentdetails = PickUpOrderPaymentDetailsRepo.GetQueryable(false).Where(t => t.PickUpOrderCode == entity.Code).ToList();
            if (paymentdetails != null && paymentdetails.Count > 0)
            {
                paymentdetails.ForEach(t =>
                {
                    paymoney += t.PayMoney;
                });
            }
            entity.ReceivedMoney = paymoney;
            entity.StillOwedMoney = entity.Money - entity.ReceivedMoney;
            if (entity.StillOwedMoney < 0)
                entity.StillOwedMoney = 0;

            if (entity.ReceivedMoney > 0 && entity.ReceivedMoney < entity.Money)
                entity.Status = EPickUpOrderStatus.UnEnough;
            else if (entity.ReceivedMoney >= entity.Money)
                entity.Status = EPickUpOrderStatus.Payed;
        }

        public bool RecountMoneySave(string code)
        {
            if (string.IsNullOrEmpty(code))
                return false;
            var entity = Repository.GetInclude(t => t.PickUpOrderItemList).FirstOrDefault(t => t.Code == code);
            if (entity == null)
                return false;
            RecountMoney(entity);
            return Repository.Update(entity) > 0;
        }

        public IEnumerable<PickUpOrder> Search(PickUpOrderFilter filter, ref int totalCount)
        {
            var queryable = Repository.GetInclude(t => t.PickUpOrderItemList, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.ID.HasValue)
                queryable = queryable.Where(t => t.ID == filter.ID.Value);
            if (!string.IsNullOrEmpty(filter.PlateNumber))
                queryable = queryable.Where(t => t.PlateNumber == filter.PlateNumber);
            if (filter.CreatedDate.HasValue)
            {
                var date = filter.CreatedDate.Value.Date;
                var nextdate = filter.CreatedDate.Value.Date.AddDays(1);
                queryable = queryable.Where(t => t.CreatedDate >= date && t.CreatedDate < nextdate);
            }
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.OrderByDescending(t => t.CreatedDate).ToArray();
        }

        /// <summary>
        /// 结账
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool CheckOut(string code)
        {
            if (string.IsNullOrEmpty(code))
                return false;
            var order = Repository.GetQueryable().Where(t => t.Code == code && t.MerchantID == AppContext.CurrentSession.MerchantID).FirstOrDefault();
            if (order == null)
                return false;
            order.Status = EPickUpOrderStatus.Payed;
            return Repository.Update(order) > 0;
        }

        /// <summary>
        /// 获取接车单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PickUpOrder GetOrder(Guid id)
        {
            return Repository.GetByKey(id);
        }

        /// <summary>
        /// 核销
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool Verification(VerificationParam param)
        {
            UnitOfWork.BeginTransaction();
            try
            {
                if (param == null || param.PickUpOrderID == null || param.CouponCodes == null || param.CouponCodes.Count < 1)
                    throw new DomainException("参数错误");
                var order = Repository.GetInclude(t => t.PickUpOrderItemList).Where(t => t.ID == param.PickUpOrderID).FirstOrDefault();
                if (order == null)
                    throw new DomainException("订单不存在");
                var now = DateTime.Now;
                var coupons = CouponRepo.GetInclude(t => t.Template, false).Where(t => param.CouponCodes.Contains(t.CouponCode) && t.EffectiveDate <= now && t.ExpiredDate > now && t.Status == ECouponStatus.Default).ToList();
                if (coupons == null || coupons.Count < 1)
                    throw new DomainException("卡券已失效");
                var orderitemcodes = order.PickUpOrderItemList.Select(t => t.ProductCode).ToList();
                if (orderitemcodes == null || orderitemcodes.Count < 1)
                    throw new DomainException("服务信息为空");
                var verificationcoupons = new List<Coupon>();
                var discountCoupons = coupons.Where(t => t.Template.CouponType == ECouponType.Discount).OrderBy(t => t.Template.CouponValue).ToList();
                if (discountCoupons != null && discountCoupons.Count > 0)
                {
                    discountCoupons.ForEach(t =>
                    {
                        var discountcodes = new List<string>();
                        var undiscountcodes = new List<string>();
                        if (!string.IsNullOrEmpty(t.Template.IncludeProducts))
                        {
                            orderitemcodes.ForEach(item =>
                            {
                                if (t.Template.IncludeProducts.Contains(item))
                                {
                                    discountcodes.Add(item);
                                }
                            });
                        }
                        else if (!string.IsNullOrEmpty(t.Template.ExcludeProducts))
                        {
                            orderitemcodes.ForEach(item =>
                            {
                                if (t.Template.ExcludeProducts.Contains(item))
                                {
                                    undiscountcodes.Add(item);
                                }
                            });
                            discountcodes = orderitemcodes.Except(undiscountcodes).ToList();
                        }
                        else
                        {
                            discountcodes = orderitemcodes;
                        }
                        var discountpickuporderitems = order.PickUpOrderItemList.Where(item => discountcodes.Contains(item.ProductCode)).ToList();
                        var applyres = ApplyDiscount(discountpickuporderitems, t, order, param);
                        if (applyres != null)
                            verificationcoupons.Add(applyres);
                    });
                }
                var memberCardVoucherInfo = new List<MemberCardVoucherInfo>();
                var voucherCoupons = coupons.Where(t => t.Template.CouponType != ECouponType.Discount).OrderBy(t => t.Template.CouponValue).ToList();
                if (voucherCoupons != null && voucherCoupons.Count > 0)
                {
                    voucherCoupons.ForEach(t =>
                    {
                        var vouchercodes = new List<string>();
                        var unvouchercodes = new List<string>();
                        if (!string.IsNullOrEmpty(t.Template.IncludeProducts))
                        {
                            orderitemcodes.ForEach(item =>
                            {
                                if (t.Template.IncludeProducts.Contains(item))
                                {
                                    vouchercodes.Add(item);
                                }
                            });
                        }
                        else if (!string.IsNullOrEmpty(t.Template.ExcludeProducts))
                        {
                            orderitemcodes.ForEach(item =>
                            {
                                if (t.Template.ExcludeProducts.Contains(item))
                                {
                                    unvouchercodes.Add(item);
                                }
                            });
                            vouchercodes = orderitemcodes.Except(unvouchercodes).ToList();
                        }
                        else
                        {
                            vouchercodes = orderitemcodes;
                        }
                        var voucherpickuporderitems = order.PickUpOrderItemList.Where(item => vouchercodes.Contains(item.ProductCode)).ToList();
                        var applyres = ApplyVoucher(voucherpickuporderitems, t, order, param, memberCardVoucherInfo);
                        if (applyres != null)
                            verificationcoupons.Add(applyres);
                    });
                }
                //var verificationcards = verificationcoupons.Where(t => t.Template.Nature == ENature.Card).ToList();
                //var vcoupons = verificationcoupons.Where(t => t.Template.Nature == ENature.Coupon).ToList();
                if (verificationcoupons.Count > 0)
                {
                    CouponService.VerifyCoupon(new VerifyCouponDto
                    {
                        CouponCodes = verificationcoupons.Select(t => t.CouponCode).ToList(),
                        VerificationMode = VIP.Domain.Enums.EVerificationMode.ScanCode,
                        DepartmentCode = param.DepartmentCode,
                        DepartmentID = param.DepartmentID,
                        MemberCardVoucherInfoList = memberCardVoucherInfo,
                    });

                    RecountMoney(order);
                    PickUpOrderItemRepo.UpdateRange(order.PickUpOrderItemList);
                    Repository.Update(order);
                    UnitOfWork.CommitTransaction();
                    return true;
                }
                throw new DomainException("没有可以核销对应服务的卡券");
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException(e.Message);
            }
        }

        private Coupon ApplyDiscount(List<PickUpOrderItem> pickUpOrderItems, Coupon coupon, PickUpOrder pickUpOrder, VerificationParam param)
        {
            if (coupon == null || pickUpOrderItems == null || pickUpOrderItems.Count < 1)
                return null;
            var oriMoney = pickUpOrderItems.GroupBy(t => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            pickUpOrderItems.ForEach(t =>
            {
                var discount = coupon.Template.CouponValue / 10;
                t.Money = Math.Ceiling(t.Money * discount);
                t.Discount = (t.Quantity * t.PriceSale) != 0 ? t.Money / (t.Quantity * t.PriceSale) : 0;
            });
            var afterMoney = pickUpOrderItems.GroupBy(t => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            var discountMoney = oriMoney - afterMoney;
            if (discountMoney < 0)
                discountMoney = 0;
            if (pickUpOrder != null)
            {
                var discountproductnames = string.Empty;
                pickUpOrderItems.ForEach(t =>
                {
                    if (string.IsNullOrEmpty(discountproductnames))
                        discountproductnames += t.ProductName;
                    else
                        discountproductnames += "、" + t.ProductName;
                });
                PickUpOrderPaymentDetailsService.Add(new PickUpOrderPaymentDetails
                {
                    PickUpOrderID = pickUpOrder.ID,
                    PickUpOrderCode = pickUpOrder.Code,
                    PayType = coupon.Template.Nature == ENature.Card ? EPayType.MemberCard : EPayType.Coupon,
                    PayMoney = discountMoney,
                    PayInfo = $"类型:折扣,标题:{coupon.Template.Title},卡券号:{coupon.CouponCode},折扣系数:{coupon.Template.CouponValue / 10},折扣金额:{discountMoney},应用服务:{discountproductnames}",
                    MemberInfo = "",
                    StaffID = param.StaffID,
                    StaffName = param.StaffName,
                });
            }
            if (discountMoney > 0)
                return coupon;
            else
                return null;
        }

        private Coupon ApplyVoucher(List<PickUpOrderItem> pickUpOrderItems, Coupon coupon, PickUpOrder pickUpOrder, VerificationParam param, List<MemberCardVoucherInfo> memberCardVoucherInfo)
        {
            if (coupon == null || pickUpOrderItems == null || pickUpOrderItems.Count < 1)
                return null;
            var totalValue = coupon.CouponValue;
            if (totalValue == 0)
                return null;
            var voucherproductnames = string.Empty;
            var oriMoney = pickUpOrderItems.GroupBy(t => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            pickUpOrderItems.ForEach(t =>
            {
                if (totalValue > 0)
                {
                    if (t.Money > 0)
                    {
                        if (t.Money <= totalValue)
                        {
                            t.Money = 0;
                            totalValue -= t.Money;
                        }
                        else
                        {
                            t.Money -= totalValue;
                            totalValue = 0;
                        }
                        if (string.IsNullOrEmpty(voucherproductnames))
                            voucherproductnames += t.ProductName;
                        else
                            voucherproductnames += "、" + t.ProductName;
                    }
                }
            });
            var afterMoney = pickUpOrderItems.GroupBy(t => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            var voucherMoney = oriMoney - afterMoney;
            if (voucherMoney < 0)
                voucherMoney = 0;
            if (pickUpOrder != null)
            {
                PickUpOrderPaymentDetailsService.Add(new PickUpOrderPaymentDetails
                {
                    PickUpOrderID = pickUpOrder.ID,
                    PickUpOrderCode = pickUpOrder.Code,
                    PayType = coupon.Template.Nature == ENature.Card ? EPayType.MemberCard : EPayType.Coupon,
                    PayMoney = voucherMoney,
                    PayInfo = $"类型:抵用,标题:{coupon.Template.Title},卡券号:{coupon.CouponCode},抵用额度:{voucherMoney},应用服务:{voucherproductnames}",
                    MemberInfo = "",
                    StaffID = param.StaffID,
                    StaffName = param.StaffName,
                });
            }
            if (coupon.Template.CouponType != ECouponType.Discount && voucherMoney > 0)
            {
                memberCardVoucherInfo.Add(new MemberCardVoucherInfo
                {
                    Code = coupon.CouponCode,
                    VoucherAmount = voucherMoney,
                });
            }
            if (voucherMoney > 0)
                return coupon;
            else
                return null;
        }
    }
}

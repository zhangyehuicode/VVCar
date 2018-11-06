using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using VVCar.BaseData.Services;
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

        IRepository<PickUpOrder> PickUpOrderRepo { get => UnitOfWork.GetRepository<IRepository<PickUpOrder>>(); }

        IRepository<OrderDividend> OrderDividendRepo { get => UnitOfWork.GetRepository<IRepository<OrderDividend>>(); }

        IRepository<PickUpOrderItem> PickUpOrderItemRepo { get => UnitOfWork.GetRepository<IRepository<PickUpOrderItem>>(); }

        IRepository<PickUpOrderTaskDistribution> PickUpOrderTaskDistributionRepo { get => UnitOfWork.GetRepository<IRepository<PickUpOrderTaskDistribution>>(); }

        IMemberService MemberService { get => ServiceLocator.Instance.GetService<IMemberService>(); }

        IRepository<CouponItem> CouponItemRepo { get => UnitOfWork.GetRepository<IRepository<CouponItem>>(); }
        IRepository<User> UserRepo { get => UnitOfWork.GetRepository<IRepository<User>>(); }

        IRepository<CouponItemVerificationRecord> CouponItemVerificationRecordRepo { get => UnitOfWork.GetRepository<IRepository<CouponItemVerificationRecord>>(); }

        ISystemSettingService SystemSettingService
        {
            get { return ServiceLocator.Instance.GetService<ISystemSettingService>(); }
        }

        IWeChatService WeChatService
        {
            get { return ServiceLocator.Instance.GetService<IWeChatService>(); }
        }

        IRepository<Member> MemberRepo { get => UnitOfWork.GetRepository<IRepository<Member>>(); }

        IRepository<Product> ProductRepo { get => UnitOfWork.GetRepository<IRepository<Product>>(); }

        IRepository<MemberCard> MemberCardRepo { get => UnitOfWork.GetRepository<IRepository<MemberCard>>(); }

        IMemberCardService MemberCardService { get => ServiceLocator.Instance.GetService<IMemberCardService>(); }

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

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override PickUpOrder Add(PickUpOrder entity)
        {
            if (entity == null)//|| entity.PickUpOrderItemList == null || entity.PickUpOrderItemList.Count < 1
                return null;
            if (string.IsNullOrEmpty(entity.PlateNumber))
                throw new DomainException("车牌号不能为空");
            UnitOfWork.BeginTransaction();
            try
            {
                entity.ID = Util.NewID();
                entity.CreatedDate = DateTime.Now;
                entity.MerchantID = AppContext.CurrentSession.MerchantID;

                if (entity.StaffID == null || entity.StaffID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    entity.StaffID = AppContext.CurrentSession.UserID;
                    entity.StaffName = AppContext.CurrentSession.UserName;
                }
                if (string.IsNullOrEmpty(entity.Code))
                    entity.Code = GetTradeNo();
                if (entity.PickUpOrderItemList != null && entity.PickUpOrderItemList.Count > 0)
                {
                    entity.PickUpOrderItemList.ForEach(t =>
                    {
                        t.ID = Util.NewID();
                        t.PickUpOrderID = entity.ID;
                        t.MerchantID = entity.MerchantID;
                        if (t.IsReduce)
                            t.Money = t.Quantity * t.ReducedPrice;
                        else
                        {
                            t.Money = t.Quantity * t.PriceSale;
                            t.ReducedPrice = t.PriceSale;
                        }
                        t.Discount = 1;
                    });
                    RecountMoney(entity);
                    PickUpOrderTaskDistribution(entity.PickUpOrderItemList.ToList());
                }
                entity.PlateNumber = entity.PlateNumber.ToUpper();
                var pickupOrder = base.Add(entity);
                UnitOfWork.CommitTransaction();
                return pickupOrder;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException(e.Message);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override bool Delete(Guid key)
        {
            var entity = Repository.GetByKey(key);
            if (entity == null)
                throw new DomainException("数据不存在");
            if (entity.Status == EPickUpOrderStatus.Payed)
                throw new DomainException("订单已付款,不能删除!");
            if (entity.Status == EPickUpOrderStatus.UnEnough)
                throw new DomainException("订单付款付款不足,不能删除!");
            entity.IsDeleted = true;
            return Repository.Update(entity) > 0;
        }

        private void PickUpOrderTaskDistribution(List<PickUpOrderItem> pickUpOrderItemList)
        {
            if (pickUpOrderItemList == null || pickUpOrderItemList.Count < 1)
                return;
            pickUpOrderItemList.ForEach(t =>
            {
                if (t.PickUpOrderTaskDistributionList.Count > 0)
                {
                    var constructionCount = t.PickUpOrderTaskDistributionList.Count(m => m.PeopleType == ETaskDistributionPeopleType.ConstructionCrew);
                    var salesmanCount = t.PickUpOrderTaskDistributionList.Count(m => m.PeopleType == ETaskDistributionPeopleType.Salesman);
                    var product = ProductRepo.GetByKey(t.ProductID, false);
                    t.PickUpOrderTaskDistributionList.ForEach(distribution =>
                    {
                        t.ConstructionCount = constructionCount;
                        t.SalesmanCount = salesmanCount;
                        distribution.ID = Util.NewID();
                        distribution.CreatedUserID = AppContext.CurrentSession.UserID;
                        distribution.CreatedUser = AppContext.CurrentSession.UserName;
                        distribution.CreatedDate = DateTime.Now;
                        distribution.MerchantID = AppContext.CurrentSession.MerchantID;
                        distribution.PickUpOrderID = t.PickUpOrderID;
                        distribution.PickUpOrderItemID = t.ID;
                        if (product != null)
                        {
                            t.IsCommissionRate = product.IsCommissionRate;
                            t.IsSalesmanCommissionRate = product.IsSalesmanCommissionRate;
                            t.CommissionRate = product.CommissionRate;
                            t.SalesmanCommissionRate = product.SalesmanCommissionRate;
                            if (distribution.PeopleType == ETaskDistributionPeopleType.ConstructionCrew)
                            {
                                distribution.IsCommissionRate = t.IsCommissionRate;
                                distribution.TotalMoney = t.Money / constructionCount;
                                if (t.IsCommissionRate)
                                {
                                    distribution.CommissionRate = product.CommissionRate;
                                    distribution.Commission = Math.Floor(distribution.TotalMoney * distribution.CommissionRate / 100);
                                }
                                else
                                {
                                    distribution.CommissionMoney = product.CommissionMoney;
                                    distribution.Commission = (product.CommissionMoney * t.Quantity) / constructionCount;
                                }
                                distribution.ConstructionCount = constructionCount;
                            }
                            if (distribution.PeopleType == ETaskDistributionPeopleType.Salesman)
                            {
                                distribution.IsCommissionRate = t.IsSalesmanCommissionRate;
                                distribution.TotalMoney = t.Money / salesmanCount;
                                if (t.IsSalesmanCommissionRate)
                                {
                                    distribution.SalesmanCommissionRate = product.SalesmanCommissionRate;
                                    distribution.SalesmanCommission = Math.Floor(distribution.TotalMoney * distribution.SalesmanCommissionRate / 100);
                                }
                                else
                                {
                                    distribution.SalesmanCommissionMoney = product.SalesmanCommissionMoney;
                                    distribution.SalesmanCommission = (product.SalesmanCommissionMoney * t.Quantity) / salesmanCount;
                                }
                                distribution.SalesmanCount = salesmanCount;
                            }
                        }
                    });
                }
            });
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
                    if (t.IsDeleted == false)
                    {
                        if (t.IsReduce)
                        {
                            totalMoney += t.Quantity * t.ReducedPrice;
                        }
                        else
                        {
                            totalMoney += t.Quantity * t.PriceSale;
                        }
                        //t.Money = t.Quantity * t.PriceSale;
                    }
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
            else if (entity.ReceivedMoney > 0 && entity.ReceivedMoney < entity.Money)
                entity.Status = EPickUpOrderStatus.UnEnough;
            else if (entity.ReceivedMoney >= entity.Money)
            {
                if (entity.PickUpOrderItemList != null && entity.PickUpOrderItemList.Count > 0)
                {
                    entity.Status = EPickUpOrderStatus.Payed;
                    AddOrderDividend(entity.Code);
                }
                else
                {
                    entity.Status = EPickUpOrderStatus.UnPay;
                }
            }
        }

        void AddOrderDividend(string code)
        {
            try
            {
                var pickUpOrder = Repository.GetInclude(t => t.PickUpOrderItemList, false).Where(t => t.Code == code).FirstOrDefault();
                var orderDividendList = new List<OrderDividend>();
                pickUpOrder.PickUpOrderItemList.ForEach(t =>
                {
                    var pickUpOrderTaskDistributionList = PickUpOrderTaskDistributionRepo.GetInclude(m => m.User, false).Where(m => m.PickUpOrderID == pickUpOrder.ID && m.PickUpOrderItemID == t.ID).ToList();
                    pickUpOrderTaskDistributionList.ForEach(distribution =>
                    {
                        var orderDividend = new OrderDividend();
                        orderDividend.TradeOrderID = pickUpOrder.ID;
                        orderDividend.TradeNo = pickUpOrder.Code;
                        orderDividend.PlateNumber = pickUpOrder.PlateNumber;
                        orderDividend.Code = t.ProductCode;
                        orderDividend.Name = t.ProductName;
                        orderDividend.GoodsID = t.ProductID;
                        orderDividend.OrderType = EShopTradeOrderType.PickupOrder;
                        orderDividend.CreatedDate = DateTime.Now;
                        orderDividend.ID = Util.NewID();
                        orderDividend.PickUpOrderTaskDistributionID = distribution.ID;
                        if (distribution.PeopleType == ETaskDistributionPeopleType.ConstructionCrew)
                        {
                            if (distribution.IsCommissionRate)
                            {
                                orderDividend.IsCommissionRate = true;
                                orderDividend.CommissionRate = distribution.CommissionRate;
                                orderDividend.Commission = distribution.Commission;
                            }
                            else
                            {
                                orderDividend.IsCommissionRate = false;
                                orderDividend.CommissionMoney = distribution.CommissionMoney;
                                orderDividend.Commission = distribution.Commission;
                            }
                        }
                        if (distribution.PeopleType == ETaskDistributionPeopleType.Salesman)
                        {
                            if (distribution.IsSalesmanCommissionRate)
                            {
                                orderDividend.IsCommissionRate = true;
                                orderDividend.CommissionRate = distribution.SalesmanCommissionRate;
                                orderDividend.Commission = distribution.SalesmanCommission;
                            }
                            else
                            {
                                orderDividend.IsCommissionRate = false;
                                orderDividend.CommissionMoney = distribution.SalesmanCommissionMoney;
                                orderDividend.Commission = distribution.SalesmanCommission;
                            }
                        }
                        orderDividend.PeopleType = distribution.PeopleType;
                        var user = UserRepo.GetByKey(distribution.UserID);
                        orderDividend.UserCode = user.Code;
                        orderDividend.UserName = user.Name;
                        orderDividend.UserID = user.ID;
                        orderDividend.Money = distribution.TotalMoney;
                        orderDividend.MerchantID = AppContext.CurrentSession.MerchantID;
                        orderDividendList.Add(orderDividend);
                    });
                });
                OrderDividendRepo.AddRange(orderDividendList);
            }
            catch (Exception e)
            {
                AppContext.Logger.Error("接车单分红记录失败:" + e.Message);
            }
        }

        public bool RecountMoneySave(string code)
        {
            if (string.IsNullOrEmpty(code))
                return false;
            var entity = Repository.GetInclude(t => t.PickUpOrderItemList, false).FirstOrDefault(t => t.Code == code);//PickUpOrderItemList PickUpOrderTaskDistributionList User
            if (entity == null)
                return false;
            RecountMoney(entity);
            return Repository.Update(entity) > 0;
        }

        public IEnumerable<PickUpOrderDto> Search(PickUpOrderFilter filter, ref int totalCount)
        {
            var queryable = Repository.GetInclude(t => t.PickUpOrderItemList, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.ID.HasValue)
                queryable = queryable.Where(t => t.ID == filter.ID.Value);
            if (!string.IsNullOrEmpty(filter.Code))
                queryable = queryable.Where(t => t.Code == filter.Code);
            if (!string.IsNullOrEmpty(filter.PlateNumber))
                queryable = queryable.Where(t => t.PlateNumber.Contains(filter.PlateNumber));
            if (!string.IsNullOrEmpty(filter.StaffName))
                queryable = queryable.Where(t => t.StaffName.Contains(filter.StaffName));
            if (filter.Status.HasValue)
                queryable = queryable.Where(t => t.Status == filter.Status);
            if (filter.CreatedDate.HasValue)
            {
                var date = filter.CreatedDate.Value.Date;
                var nextdate = filter.CreatedDate.Value.Date.AddDays(1);
                queryable = queryable.Where(t => t.CreatedDate >= date && t.CreatedDate < nextdate);
            }
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            var result = queryable.MapTo<PickUpOrderDto>().OrderByDescending(t => t.CreatedDate).ToArray();
            result.ForEach(t =>
            {
                if (t.MemberID != null && t.MemberID != Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    var member = MemberRepo.GetByKey(t.MemberID);
                    if (member != null)
                    {
                        t.MemberName = member.Name;
                        t.MemberMobilePhoneNo = member.MobilePhoneNo;
                        var memberCard = MemberCardRepo.GetByKey(member.CardID);
                        if (memberCard != null)
                        {
                            t.CardNumber = memberCard.Code;
                            t.CardStatus = memberCard.Status == ECardStatus.Activated ? "已激活" : memberCard.Status == ECardStatus.Lost ? "挂失" : "未激活";
                            t.EffectiveDate = memberCard.EffectiveDate;
                            t.CardBalance = memberCard.CardBalance;
                        }
                        else
                        {
                            t.CardNumber = "未找到储值卡";
                        }
                    }
                    else
                    {
                        throw new DomainException("会员不存在");
                    }
                }
            });
            return result;
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
        /// 获取会员接车单
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public IEnumerable<PickUpOrderDto> GetMemberPickUpOrder(Guid memberId)
        {
            var result = new List<PickUpOrderDto>();
            if (memberId == null)
                return result;
            var member = MemberRepo.GetInclude(t => t.MemberPlateList, false).Where(t => t.ID == memberId).FirstOrDefault();
            if (member != null && member.MemberPlateList != null)
            {
                var platenumbers = member.MemberPlateList.Select(t => t.PlateNumber).ToList();
                result = Repository.GetIncludes(false, "PickUpOrderItemList", "Merchant").Where(t => platenumbers.Contains(t.PlateNumber)).OrderByDescending(t => t.CreatedDate).ToList().MapTo<List<PickUpOrderDto>>();
            }
            return result;
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
                if (param == null || param.PickUpOrderID == null || ((param.CouponCodes == null || param.CouponCodes.Count < 1) && (param.MemberCardNumbers == null || param.MemberCardNumbers.Count < 1)))
                    throw new DomainException("参数错误");
                var order = Repository.GetInclude(t => t.PickUpOrderItemList).Where(t => t.ID == param.PickUpOrderID).FirstOrDefault();
                if (order == null)
                    throw new DomainException("订单不存在");
                var noVerificationCoupons = false;
                if (param.CouponCodes != null && param.CouponCodes.Count > 0)
                {
                    var now = DateTime.Now;
                    var coupons = CouponRepo.GetInclude(t => t.Template, false).Where(t => param.CouponCodes.Contains(t.CouponCode) && t.EffectiveDate <= now && t.ExpiredDate > now && t.Status == ECouponStatus.Default).ToList();
                    if (coupons == null || coupons.Count < 1)
                        throw new DomainException("卡券已失效");
                    var orderitemcodes = order.PickUpOrderItemList.Select(t => t.ProductCode).ToList();
                    if (orderitemcodes == null || orderitemcodes.Count < 1)
                        throw new DomainException("服务信息为空");
                    var verificationcoupons = new List<Coupon>();
                    var verificationcards = new List<Coupon>();
                    var discountCoupons = coupons.Where(t => t.Template.CouponType == ECouponType.Discount).OrderBy(t => t.Template.CouponValue).ToList();
                    if (discountCoupons != null && discountCoupons.Count > 0)
                    {
                        discountCoupons.ForEach(t =>
                        {
                            if (t.Template.Nature == ENature.Coupon)
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
                            }
                            else
                            {
                                var applycard = ApplyCardDiscount(t, order, param);
                                if (applycard != null)
                                    verificationcards.Add(applycard);
                            }
                        });
                    }
                    var memberCardVoucherInfo = new List<MemberCardVoucherInfo>();
                    var voucherCoupons = coupons.Where(t => t.Template.CouponType != ECouponType.Discount).OrderBy(t => t.Template.CouponValue).ToList();
                    if (voucherCoupons != null && voucherCoupons.Count > 0)
                    {
                        voucherCoupons.ForEach(t =>
                        {
                            if (t.Template.Nature == ENature.Coupon)
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
                            }
                            else
                            {
                                var applycard = ApplyCardVoucher(t, order, param);
                                if (applycard != null)
                                    verificationcards.Add(applycard);
                            }
                        });
                    }
                    if (verificationcoupons.Count > 0)
                    {
                        //券核销
                        CouponService.VerifyCoupon(new VerifyCouponDto
                        {
                            CouponCodes = verificationcoupons.Where(t => t.Template.Nature == ENature.Coupon).Select(t => t.CouponCode).ToList(),
                            VerificationMode = VIP.Domain.Enums.EVerificationMode.ScanCode,
                            DepartmentCode = param.DepartmentCode,
                            DepartmentID = param.DepartmentID,
                            MemberCardVoucherInfoList = memberCardVoucherInfo,
                            ConsumeMoney = order.Money,
                        });
                    }
                    //if (verificationcoupons.Count > 0 || verificationcards.Count > 0)
                    //{
                    //    //RecountMoney(order);
                    //    //PickUpOrderItemRepo.UpdateRange(order.PickUpOrderItemList);
                    //    //Repository.Update(order);
                    //    //return true;
                    //}
                    //else
                    //{
                    //    noVerificationCoupons = true;
                    //    //throw new DomainException("没有可以核销对应服务的卡券");
                    //}
                    if (verificationcoupons.Count < 1 && verificationcards.Count < 1)
                        noVerificationCoupons = true;
                }
                var usedcard = 0;
                if (param.MemberCardNumbers != null && param.MemberCardNumbers.Count > 0)
                {
                    usedcard = ApplyMemberCard(order, param);
                }
                if (noVerificationCoupons && usedcard < 1)
                {
                    throw new DomainException("没有可以核销的卡券");
                }
                RecountMoney(order);
                PickUpOrderItemRepo.UpdateRange(order.PickUpOrderItemList);
                Repository.Update(order);
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException(e.Message);
            }
        }

        /// <summary>
        /// 储值卡核销
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public PickUpOrder VerificationByMemberCard(VerificationByMemberCardParam param)
        {
            if (param.PickUpOrderID == null)
            {
                throw new DomainException("参数错误");
            }
            UnitOfWork.BeginTransaction();
            try
            {
                var member = MemberRepo.GetByKey(param.MemberID);
                if (member == null)
                    throw new DomainException("会员不存在");
                var memberCard = MemberCardRepo.GetByKey(member.CardID);
                if (memberCard.CardBalance < param.PayMoney)
                {
                    throw new DomainException("储值卡余额不足");
                }
                memberCard.CardBalance -= param.PayMoney;

                var cardTradeResult = MemberCardService.Consume(new ConsumeInfoDto
                {
                    CardNumber = memberCard.Code,
                    OutTradeNo = param.Code,
                    TradeAmount = param.PayMoney,
                    UseBalanceAmount = 0,
                    OperateUser = param.StaffName,
                }, ETradeSource.WeChat);

                PickUpOrderPaymentDetailsService.Add(new PickUpOrderPaymentDetails
                {
                    PickUpOrderID = param.PickUpOrderID,
                    PickUpOrderCode = param.Code,
                    PayType = EPayType.ValueCard,
                    PayMoney = param.PayMoney,
                    PayInfo = "储值卡支付:" + param.PayMoney + "元",
                    MemberInfo = "",
                    StaffID = param.StaffID,
                    StaffName = param.StaffName,
                });
                UnitOfWork.CommitTransaction();
                var pickUpOrder = PickUpOrderRepo.GetByKey(param.PickUpOrderID);
                SendMemberCardUsedNotify(member, param.Code, "接车单", param.PayMoney, memberCard.CardBalance);
                return pickUpOrder;
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
            {
                if (coupon.Template.ConsumePointRate > 0)
                {
                    var sendpoint = discountMoney * coupon.Template.ConsumePointRate / 100;
                    MemberService.AdjustMemberPoint(coupon.OwnerOpenID, EMemberPointType.MemberCardConsumeReturn, (double)sendpoint);
                }
                return coupon;
            }
            else
                return null;
        }

        private Coupon ApplyCardDiscount(Coupon coupon, PickUpOrder pickUpOrder, VerificationParam param)
        {
            if (coupon == null || pickUpOrder == null || pickUpOrder.PickUpOrderItemList == null || pickUpOrder.PickUpOrderItemList.Count < 1 || param == null)
                return null;

            try
            {
                CouponService.CheckCoupon(coupon, "", VIP.Domain.Enums.EVerificationMode.ScanCode, pickUpOrder.Money);
            }
            catch
            {
                return null;
            }

            var couponItems = CouponItemRepo.GetQueryable().Where(t => t.CouponID == coupon.ID).ToList();
            if (couponItems == null || couponItems.Count < 1)
                return null;

            var availabletimes = couponItems.GroupBy(g => 1).Select(t => t.Sum(s => s.Quantity)).FirstOrDefault();
            if (availabletimes < 1)
                return null;
            var couponItemProductCodes = couponItems.Select(t => t.ProductCode).ToList();

            var oriMoney = pickUpOrder.PickUpOrderItemList.GroupBy(t => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            pickUpOrder.PickUpOrderItemList.Where(t => couponItemProductCodes.Contains(t.ProductCode)).ForEach(t =>
             {
                 var couponItem = couponItems.Where(item => item.ProductCode == t.ProductCode).FirstOrDefault();
                 if (couponItem != null && couponItem.Quantity > 0)
                 {
                     var discount = coupon.CouponValue / 10;
                     t.Money = t.Money * discount;//Math.Ceiling(t.Money * discount);
                     t.Discount = (t.Quantity * t.PriceSale) != 0 ? t.Money / (t.Quantity * t.PriceSale) : 0;

                     CouponItemVerificationRecordRepo.Add(new CouponItemVerificationRecord
                     {
                         ID = Util.NewID(),
                         CouponID = coupon.ID,
                         CouponItemID = couponItem.ID,
                         PickUpOrderID = pickUpOrder.ID,
                         TradeNo = pickUpOrder.Code,
                         Quantity = 1,
                         CreatedDate = DateTime.Now,
                     });

                     couponItem.Quantity -= 1;
                 }
             });
            CouponItemRepo.UpdateRange(couponItems);

            var afterMoney = pickUpOrder.PickUpOrderItemList.GroupBy(t => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            var discountMoney = oriMoney - afterMoney;
            if (discountMoney < 0)
                discountMoney = 0;
            var discountproductnames = string.Empty;
            if (pickUpOrder != null && discountMoney > 0)
            {
                pickUpOrder.PickUpOrderItemList.Where(t => couponItemProductCodes.Contains(t.ProductCode)).ForEach(t =>
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
                    PayType = EPayType.MemberCard,
                    PayMoney = discountMoney,
                    PayInfo = $"类型:折扣,标题:{coupon.Template.Title},卡号:{coupon.CouponCode},折扣系数:{coupon.Template.CouponValue / 10},折扣金额:{discountMoney},应用服务:{discountproductnames}",
                    MemberInfo = "",
                    StaffID = param.StaffID,
                    StaffName = param.StaffName,
                });
            }
            if (discountMoney > 0)
            {
                if (coupon.Template.ConsumePointRate > 0)
                {
                    var sendpoint = discountMoney * coupon.Template.ConsumePointRate / 100;
                    MemberService.AdjustMemberPoint(coupon.OwnerOpenID, EMemberPointType.MemberCardConsumeReturn, (double)sendpoint);
                }
                SendCardUsedNotify(coupon, discountproductnames, coupon.CouponValue);
                return coupon;
            }
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
            {
                if (coupon.Template.ConsumePointRate > 0)
                {
                    var sendpoint = voucherMoney * coupon.Template.ConsumePointRate / 100;
                    MemberService.AdjustMemberPoint(coupon.OwnerOpenID, EMemberPointType.MemberCardConsumeReturn, (double)sendpoint);
                }
                return coupon;
            }
            else
                return null;
        }

        private Coupon ApplyCardVoucher(Coupon coupon, PickUpOrder pickUpOrder, VerificationParam param)
        {
            if (coupon == null || pickUpOrder == null || pickUpOrder.PickUpOrderItemList == null || pickUpOrder.PickUpOrderItemList.Count < 1 || param == null)
                return null;

            try
            {
                CouponService.CheckCoupon(coupon, "", VIP.Domain.Enums.EVerificationMode.ScanCode, pickUpOrder.Money);
            }
            catch
            {
                return null;
            }

            var couponItems = CouponItemRepo.GetQueryable().Where(t => t.CouponID == coupon.ID).ToList();
            if (couponItems == null || couponItems.Count < 1)
                return null;

            var availabletimes = couponItems.GroupBy(g => 1).Select(t => t.Sum(s => s.Quantity)).FirstOrDefault();
            if (availabletimes < 1)
                return null;
            var couponItemProductCodes = couponItems.Select(t => t.ProductCode).ToList();

            var voucherproductnames = string.Empty;
            var oriMoney = pickUpOrder.PickUpOrderItemList.GroupBy(t => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            pickUpOrder.PickUpOrderItemList.Where(t => couponItemProductCodes.Contains(t.ProductCode)).ForEach(t =>
             {
                 if (t.Money > 0)
                 {
                     var couponItem = couponItems.Where(item => item.ProductCode == t.ProductCode).FirstOrDefault();
                     if (couponItem != null && couponItem.Quantity > 0)
                     {
                         t.Money = 0;
                         couponItem.Quantity -= 1;

                         CouponItemVerificationRecordRepo.Add(new CouponItemVerificationRecord
                         {
                             ID = Util.NewID(),
                             CouponID = coupon.ID,
                             CouponItemID = couponItem.ID,
                             PickUpOrderID = pickUpOrder.ID,
                             TradeNo = pickUpOrder.Code,
                             Quantity = 1,
                             CreatedDate = DateTime.Now,
                         });

                         if (string.IsNullOrEmpty(voucherproductnames))
                             voucherproductnames += t.ProductName;
                         else
                             voucherproductnames += "、" + t.ProductName;
                     }
                 }
             });
            CouponItemRepo.UpdateRange(couponItems);

            var afterMoney = pickUpOrder.PickUpOrderItemList.GroupBy(t => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            var voucherMoney = oriMoney - afterMoney;
            if (voucherMoney < 0)
                voucherMoney = 0;
            if (pickUpOrder != null && voucherMoney > 0)
            {
                PickUpOrderPaymentDetailsService.Add(new PickUpOrderPaymentDetails
                {
                    PickUpOrderID = pickUpOrder.ID,
                    PickUpOrderCode = pickUpOrder.Code,
                    PayType = EPayType.MemberCard,
                    PayMoney = voucherMoney,
                    PayInfo = $"类型:抵用,标题:{coupon.Template.Title},卡号:{coupon.CouponCode},抵用额度:{voucherMoney},应用服务:{voucherproductnames}",
                    MemberInfo = "",
                    StaffID = param.StaffID,
                    StaffName = param.StaffName,
                });
            }
            if (voucherMoney > 0)
            {
                if (coupon.Template.ConsumePointRate > 0)
                {
                    var sendpoint = voucherMoney * coupon.Template.ConsumePointRate / 100;
                    MemberService.AdjustMemberPoint(coupon.OwnerOpenID, EMemberPointType.MemberCardConsumeReturn, (double)sendpoint);
                }
                SendCardUsedNotify(coupon, voucherproductnames, voucherMoney);
                return coupon;
            }
            else
                return null;
        }

        private int ApplyMemberCard(PickUpOrder pickUpOrder, VerificationParam param)
        {
            if (pickUpOrder == null || pickUpOrder.PickUpOrderItemList == null || pickUpOrder.PickUpOrderItemList.Count < 1 || param == null || param.MemberCardNumbers == null || param.MemberCardNumbers.Count() < 1)
                return 0;
            var usedcard = 0;
            foreach (var cardnumber in param.MemberCardNumbers)
            {
                var card = MemberCardRepo.GetQueryable(false).Where(t => t.Code == cardnumber).FirstOrDefault();
                if (card == null)
                    continue;
                decimal deductionMoney = 0;
                decimal totalMoney = 0;
                pickUpOrder.PickUpOrderItemList.ForEach(t =>
                {
                    totalMoney += t.Quantity * t.PriceSale;
                });
                decimal paymoney = 0;
                var paymentdetails = PickUpOrderPaymentDetailsRepo.GetQueryable(false).Where(t => t.PickUpOrderCode == pickUpOrder.Code).ToList();
                if (paymentdetails != null && paymentdetails.Count > 0)
                {
                    paymentdetails.ForEach(t =>
                    {
                        paymoney += t.PayMoney;
                    });
                }
                var stillowemoney = totalMoney - paymoney;
                if (stillowemoney <= 0)
                    break;
                if (stillowemoney >= card.CardBalance)
                {
                    deductionMoney = card.CardBalance;
                }
                else
                {
                    deductionMoney = stillowemoney;
                }
                if (deductionMoney <= 0)
                    break;
                var cardTradeResult = MemberCardService.Consume(new ConsumeInfoDto
                {
                    CardNumber = cardnumber,
                    OutTradeNo = pickUpOrder.Code,
                    TradeAmount = deductionMoney,
                    UseBalanceAmount = deductionMoney,
                    OperateUser = param.StaffName,
                }, ETradeSource.WeChat);
                PickUpOrderPaymentDetailsService.Add(new PickUpOrderPaymentDetails
                {
                    PickUpOrderID = pickUpOrder.ID,
                    PickUpOrderCode = pickUpOrder.Code,
                    PayType = EPayType.ValueCard,
                    PayMoney = deductionMoney,
                    PayInfo = $"类型:储值卡,卡号:{cardnumber},抵扣金额:{deductionMoney}",
                    MemberInfo = cardTradeResult.MemberName,
                    StaffID = param.StaffID,
                    StaffName = param.StaffName,
                });
                usedcard++;
            }
            return usedcard;
        }

        /// <summary>
        /// 发送会员卡使用通知
        /// </summary>
        /// <param name="coupon"></param>
        void SendCardUsedNotify(Coupon coupon, string verificationItems, decimal voucherAmount = 0)
        {
            if (string.IsNullOrEmpty(coupon.OwnerOpenID) || string.IsNullOrEmpty(verificationItems) || voucherAmount <= 0)
                return;
            var templateId = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_VerificationSuccess);
            var message = new WeChatTemplateMessageDto
            {
                touser = coupon.OwnerOpenID,
                template_id = templateId,
                url = $"{AppContext.Settings.SiteDomain}/Mobile/Customer/MemberCard?mch={AppContext.CurrentSession.CompanyCode}",
                data = new System.Dynamic.ExpandoObject(),
            };
            var couponValueUnit = coupon.Template.CouponType == ECouponType.Discount ? "折" : "元";
            message.data.first = new WeChatTemplateMessageDto.MessageData(string.Format("您好，您已成功使用{0}！", coupon.Template.Title));
            message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(coupon.Template.Title);
            message.data.keyword3 = new WeChatTemplateMessageDto.MessageData($"{voucherAmount.ToString("0.##")}{couponValueUnit}");
            message.data.remark = new WeChatTemplateMessageDto.MessageData($"核销项目：{verificationItems}。感谢您的使用，欢迎下次光临");
            WeChatService.SendWeChatNotifyAsync(message);
        }

        /// <summary>
        /// 发送储值卡使用通知
        /// </summary>
        /// <param name="member"></param>
        /// <param name="title"></param>
        /// <param name="code"></param>
        /// <param name="payMoney"></param>
        /// <param name="keepMoney"></param>
        void SendMemberCardUsedNotify(Member member, string title, string code, decimal payMoney = 0, decimal keepMoney = 0)
        {
            var templateId = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_VerificationSuccess);
            var message = new WeChatTemplateMessageDto
            {
                touser = member.WeChatOpenID,
                template_id = templateId,
                url = $"{AppContext.Settings.SiteDomain}/Mobile/Customer/MemberCard?mch={AppContext.CurrentSession.CompanyCode}",
                data = new System.Dynamic.ExpandoObject(),
            };
            message.data.first = new WeChatTemplateMessageDto.MessageData(($"您好,您已使用储值卡完成核销{title}!"));
            message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            message.data.keyword2 = new WeChatTemplateMessageDto.MessageData($"{title}:{code}！");
            message.data.keyword3 = new WeChatTemplateMessageDto.MessageData($"{payMoney.ToString("0.##")}元", "#FF4040");
            message.data.remark = new WeChatTemplateMessageDto.MessageData($"卡内余额:{keepMoney.ToString("0.##")}元,感恩惠顾，期待下次再为您服务");
            WeChatService.SendWeChatNotifyAsync(message);
        }
    }
}

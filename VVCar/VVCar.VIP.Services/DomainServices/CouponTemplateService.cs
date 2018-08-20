using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.VIP.Domain;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 卡券模板 领域服务
    /// </summary>
    public partial class CouponTemplateService : DomainServiceBase<IRepository<CouponTemplate>, CouponTemplate, Guid>, ICouponTemplateService
    {
        public CouponTemplateService()
        {
        }

        #region properties

        IRepository<Coupon> CouponRepo
        {
            get { return UnitOfWork.GetRepository<IRepository<Coupon>>(); }
        }

        IRepository<Department> DepartmentRepo
        {
            get { return UnitOfWork.GetRepository<IRepository<Department>>(); }
        }

        /// <summary>
        /// 编码规则
        /// </summary>
        IMakeCodeRuleService MakeCodeRuleService
        {
            get { return ServiceLocator.Instance.GetService<IMakeCodeRuleService>(); }
        }

        IRepository<CouponTemplateUseTime> CouponTemplateUseTimeRepo
        {
            get { return UnitOfWork.GetRepository<IRepository<CouponTemplateUseTime>>(); }
        }

        ICouponTemplateUseTimeService CouponTemplateUseTimeService
        {
            get { return ServiceLocator.Instance.GetService<ICouponTemplateUseTimeService>(); }
        }

        IRepository<CouponTemplateStock> CouponTemplateStockRepo
        {
            get { return UnitOfWork.GetRepository<IRepository<CouponTemplateStock>>(); }
        }

        ICouponTemplateStockService CouponTemplateStockService
        {
            get { return ServiceLocator.Instance.GetService<ICouponTemplateStockService>(); }
        }

        #endregion properties

        public override CouponTemplate Add(CouponTemplate entity)
        {
            entity.ID = Util.NewID();
            entity.TemplateCode = MakeCodeRuleService.GenerateCode(MakeCodeTypes.CouponTemplateCode);
            entity.IsAvailable = true;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedDate = DateTime.Now;
            entity.ApproveStatus = EApproveStatus.Approved;
            if (entity.UseTimeList != null && entity.UseTimeList.Count > 0)
            {
                entity.UseTimeList.ForEach(ut => ut.ID = Util.NewID());
            }
            if (entity.IsSpecialCoupon)
            {
                entity.VerificationMode = Domain.Enums.EVerificationMode.ScanCode;
            }
            if (string.IsNullOrEmpty(entity.CoverImage))
            {
                if (entity.Nature == ENature.Coupon)
                    entity.CoverImage = "/Areas/resource/img/mobile/defaultimg1.png";
                else
                    entity.CoverImage = "/Areas/resource/img/mobile/defaultimg3.png";
            }
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        public override bool Update(CouponTemplate entity)
        {
            if (entity == null)
            {
                return false;
            }
            var coupontemplate = Repository.GetByKey(entity.ID);
            if (coupontemplate == null)
            {
                return false;
            }
            entity.TemplateCode = coupontemplate.TemplateCode;
            entity.IsAvailable = true;
            entity.CreatedUserID = coupontemplate.CreatedUserID;
            entity.CreatedUser = coupontemplate.CreatedUser;
            entity.CreatedDate = coupontemplate.CreatedDate;

            var usetime = CouponTemplateUseTimeRepo.GetQueryable(false).Where(t => t.TemplateID == coupontemplate.ID).ToArray();
            usetime.ForEach(item =>
            {
                CouponTemplateUseTimeService.Delete(item.ID);
            });
            if (entity.UseTimeList.Count > 0)
            {
                entity.UseTimeList.ForEach(item =>
                {
                    var newUseTime = new CouponTemplateUseTime
                    {
                        ID = Util.NewID(),
                        TemplateID = coupontemplate.ID,
                        BeginTime = item.BeginTime,
                        EndTime = item.EndTime,
                        Type = item.Type
                    };
                    CouponTemplateUseTimeService.Add(newUseTime);
                });
            }

            var stock = CouponTemplateStockRepo.GetByKey(coupontemplate.ID);
            if (stock != null)
            {
                stock.Stock = entity.Stock.Stock;
                stock.CollarQuantityLimit = entity.Stock.CollarQuantityLimit;
                stock.IsNoCollarQuantityLimit = entity.Stock.IsNoCollarQuantityLimit;
                //stock.UsedStock = 0;
                CouponTemplateStockService.Update(stock);
            }
            entity.ApproveStatus = EApproveStatus.Approved;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Update(entity);
        }

        public override bool Delete(Guid key)
        {
            var entity = this.Get(key);
            if (entity.ApproveStatus == EApproveStatus.Delivered)
            {
                return false;
            }
            if (entity != null)
            {
                entity.IsDeleted = true;
                return base.Update(entity);
            }
            return false;
        }

        public bool UpdateStatus(CouponTemplateDto entity)
        {
            var coupontemplate = this.Get(entity.ID);
            if (coupontemplate != null)
            {
                if (coupontemplate.IsSpecialCoupon && ((coupontemplate.PutInStartDate.HasValue && coupontemplate.PutInStartDate.Value > DateTime.Today)
                         || (coupontemplate.PutInEndDate.HasValue && coupontemplate.PutInEndDate.Value < DateTime.Today)))
                {
                    throw new DomainException($"券 {coupontemplate.Title} 当前时间不能领取");
                }
                coupontemplate.ApproveStatus = entity.AproveStatus;
                return base.Update(coupontemplate);
            }
            return false;
        }

        public bool UpdateAproveStatus(CouponTemplateDto entity)
        {
            var coupontemplate = this.Get(entity.ID);
            if (coupontemplate != null)
            {
                coupontemplate.ApproveStatus = entity.AproveStatus;
                return base.Update(coupontemplate);
            }
            return false;
        }

        public IEnumerable<CouponTemplate> Query(CouponTemplateFilter filter, out int totalCount)
        {
            var queryable = this.Repository.GetQueryable(false);
            if (filter.CouponType != -1)
            {
                queryable = queryable.Where(c => c.CouponType == (ECouponType)filter.CouponType);
            }
            if (!string.IsNullOrEmpty(filter.Title))
            {
                queryable = queryable.Where(c => c.Title.Contains(filter.Title));
            }
            if(filter.IsPutApplet.HasValue)
            {
                queryable = queryable.Where(c => c.IsPutApplet == filter.IsPutApplet.Value);
            }
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
            {
                queryable = queryable.Skip(filter.Start.Value).Take(filter.Limit.Value);
            }
            return queryable.ToArray();
        }

        public IEnumerable<CouponTemplateDto> CouponTemplateInfo(CouponTemplateFilter filter, out int totalCount)
        {
            var queryable = this.Repository.GetIncludes(false, "Stock", "UseTimeList").Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.CouponType != -1)
            {
                queryable = queryable.Where(c => c.CouponType == (ECouponType)filter.CouponType);
            }
            if (filter.AproveStatus != -2)
            {
                queryable = queryable.Where(c => c.ApproveStatus == (EApproveStatus)filter.AproveStatus);
            }
            if (!string.IsNullOrEmpty(filter.Title))
            {
                queryable = queryable.Where(c => c.Title.Contains(filter.Title));
            }
            if (filter.HiddenExpirePutInDate)
            {
                var nowDate = DateTime.Now;
                queryable = queryable.Where(c => !c.PutInEndDate.HasValue || (c.PutInEndDate.HasValue && c.PutInEndDate >= nowDate));
            }
            if (filter.IsNotSpecialCoupon)
            {
                queryable = queryable.Where(t => !t.IsSpecialCoupon);
            }
            if (filter.Nature != -1)
            {
                queryable = queryable.Where(t => t.Nature == (ENature)filter.Nature);
            }
            if (filter.IsStockholderCard.HasValue)
            {
                queryable = queryable.Where(t => t.IsStockholderCard == filter.IsStockholderCard);
            }
            if (filter.IsPutApplet.HasValue)
            {
                queryable = queryable.Where(t => t.IsPutApplet == filter.IsPutApplet);
            }
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
            {
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            }
            var couponQueryable = CouponRepo.GetQueryable(false);
            var result = queryable.ToArray().Select(c => new CouponTemplateDto
            {
                ID = c.ID,
                IsRecommend = c.IsRecommend,
                CouponType = (int)c.CouponType,
                Nature = c.Nature,
                CouponTypeName = this.GetEnumDescription(c.CouponType),
                CouponCode = couponQueryable.Where(cou => cou.TemplateID == c.ID).FirstOrDefault() != null ? couponQueryable.Where(cou => cou.TemplateID == c.ID).FirstOrDefault().CouponCode : string.Empty,
                TemplateCode = c.TemplateCode,
                Title = c.Title,
                Validity = c.IsFiexedEffectPeriod ?
                    c.EffectiveDate.Value.ToDateString() + "~" + c.ExpiredDate.Value.ToDateString()
                    : $"领取后{c.EffectiveDaysAfterReceived.GetValueOrDefault()}天生效,{c.EffectiveDays.GetValueOrDefault()}天有效",
                EffectiveDate = c.GetEffectiveDate().ToDateString(),
                ExpiredDate = c.GetExpiredDate().ToDateString(),
                PutInStartDate = c.PutInStartDate.HasValue ? c.PutInStartDate.GetValueOrDefault().ToDateString() : "",
                PutInEndDate = c.PutInEndDate.HasValue ? c.PutInEndDate.GetValueOrDefault().ToDateString() : "",
                AproveStatus = c.ApproveStatus,
                AproveStatusText = this.GetEnumDescription(c.ApproveStatus),
                Stock = c.Stock.Stock,
                FreeStock = c.Stock.FreeStock,
                CreatedDate = c.CreatedDate,
                IsSpecialCoupon = c.IsSpecialCoupon,
                Color = c.Color,
                CouponValue = c.CouponValue,
                SubTitle = c.SubTitle,
                CoverImage = c.CoverImage,
                UseInstructions = c.UseInstructions,
                MerchantPhoneNo = c.MerchantPhoneNo,
                OperationTips = c.OperationTips,
                VerificationModeDesc = GetEnumDescription(c.VerificationMode),
                IsMinConsumeLimit = c.IsMinConsumeLimit,
                MinConsume = c.MinConsume,
                IsExclusive = c.IsExclusive,
                MerchantServiceDesc = GetMerchantServiceDesc(c.MerchantService),
                ApplyStoresName = GetDepartmentInfo(c.ApplyStores),
                IsApplyAllStore = c.IsApplyAllStore,
                CollarQuantityLimit = c.Stock.CollarQuantityLimit,
                UseTimeList = c.UseTimeList,
                UseDaysOfWeek = c.UseDaysOfWeek,
                PutInUseDaysOfWeek = c.PutInUseDaysOfWeek,
                IsPutaway = c.IsPutaway,
                IncludeProductNames = c.IncludeProducts,//!= null ? GetProductNames(c.IncludeProducts) : string.Empty,
                ExcludeProductNames = c.ExcludeProducts,//!= null ? GetProductNames(c.ExcludeProducts) : string.Empty,
                IncludeProducts = c.IncludeProducts,
                ExcludeProducts = c.ExcludeProducts,
                EffectiveDaysAfterReceived = c.EffectiveDaysAfterReceived,
                EffectiveDays = c.EffectiveDays,
                CoverIntro = c.CoverIntro,
                IntroDetail = c.IntroDetail,
                MerchantService = c.MerchantService,
                IsNoCollarQuantityLimit = c.Stock.IsNoCollarQuantityLimit,
                CanShareByPeople = c.CanShareByPeople,
                VerificationMode = c.VerificationMode,
                ApplyStores = c.ApplyStores,
                PutawayTime = c.PutawayTime != null ? ((DateTime)c.PutawayTime).ToString("yyyy-MM-dd") : string.Empty,
                SoldOutTime = c.SoldOutTime != null ? ((DateTime)c.SoldOutTime).ToString("yyyy-MM-dd") : string.Empty,
                IsFiexedEffectPeriod = c.IsFiexedEffectPeriod,
                IsUseAllTime = c.IsUseAllTime,
                CanGiveToPeople = c.CanGiveToPeople,
                Remark = c.Remark,
                PutInIsUseAllTime = c.PutInIsUseAllTime,
                IsDeductionFirst = c.IsDeductionFirst,
                ConsumePointRate = c.ConsumePointRate,
                DiscountRate = c.DiscountRate,
                PriceSale = c.PriceSale,
                IsApplyAllProduct = c.IsApplyAllProduct,
            }).ToArray();
            return result.OrderByDescending(t => t.CreatedDate);
        }

        //private string GetProductNames(string productCodes)
        //{
        //    if (string.IsNullOrEmpty(productCodes))
        //    {
        //        return string.Empty;
        //    }
        //    var requestUri = string.Format("{0}/OpenApi/Member/GetProducts?productCodes={1}",
        //        AppContext.Settings.PosService, productCodes);
        //    var httpClient = new HttpClient();
        //    httpClient.DefaultRequestHeaders.Add(Constants.HttpHeaderCompanyCode, AppContext.Settings.CompanyCode);
        //    var response = httpClient.GetStringAsync(requestUri).Result;
        //    var result = JsonHelper.FromJson<PagedActionResult<ProductDto>>(response);
        //    if (!result.IsSuccessful)
        //    {
        //        AppContext.Logger.Error("GetProductNames has error. ErrorMessage:{0}", result.ErrorMessage);
        //        return string.Empty;
        //    }
        //    var productNames = string.Empty;
        //    result.Data.ForEach(item =>
        //    {
        //        productNames += item.Name + ',';
        //    });
        //    if (productNames.Length > 0)
        //    {
        //        productNames = productNames.Substring(0, productNames.Length - 1);
        //    }
        //    return productNames;
        //}

        /// <summary>
        /// 有效劵信息
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<CouponTemplateDto> GetValidCouponTemplateInfo(CouponTemplateFilter filter, out int totalCount)
        {
            var queryable = this.Repository.GetIncludes(false, "Stock", "UseTimeList");
            var dateNow = DateTime.Now;

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Title))
                {
                    queryable = queryable.Where(t => t.Title.Contains(filter.Title));
                }
                if (!string.IsNullOrEmpty(filter.TemplateCodeOrTitle))
                {
                    queryable = queryable.Where(t => t.TemplateCode.Contains(filter.TemplateCodeOrTitle) || t.Title.Contains(filter.TemplateCodeOrTitle));
                }
                if (filter.Nature != -1)
                {
                    queryable = queryable.Where(t => t.Nature == (ENature)filter.Nature);
                }
            }
            var result = queryable.Where(c => c.ApproveStatus == EApproveStatus.Delivered && !c.IsSpecialCoupon && ((c.EffectiveDate != null && c.ExpiredDate > dateNow) || c.EffectiveDate == null) && (c.PutInStartDate != null && c.PutInStartDate <= dateNow) && (c.PutInEndDate != null && c.PutInEndDate > dateNow)).ToArray();
            result = result.Where(t => t.Stock.FreeStock > 0).ToArray();

            totalCount = result.Count();

            return result.Select(c => new CouponTemplateDto
            {
                ID = c.ID,
                CouponTypeName = this.GetEnumDescription(c.CouponType),
                TemplateCode = c.TemplateCode,
                Title = c.Title,
                Validity = c.IsFiexedEffectPeriod ? ((DateTime)c.EffectiveDate).ToString("yyyy-MM-dd") + "~" + ((DateTime)c.ExpiredDate).ToString("yyyy-MM-dd") : "领取后" + (c.EffectiveDaysAfterReceived != null ? c.EffectiveDaysAfterReceived : 0) + "天生效," + (c.EffectiveDays != null ? c.EffectiveDays : 0) + "天有效",
                AproveStatusText = this.GetEnumDescription(c.ApproveStatus),
                FreeStock = c.Stock.FreeStock,
                ExpiredDate = c.GetExpiredDate().ToDateString(),
                IsFiexedEffectPeriod = c.IsFiexedEffectPeriod,
            }).OrderByDescending(t => t.CreatedDate);
        }

        /// <summary>
        /// 获取可以投放的优惠券模板列表
        /// </summary>
        /// <returns></returns>
        public IList<CouponTemplateDto> GetCanDeliveryCouponTemplateList()
        {
            var dateNow = DateTime.Today;
            var queryable = Repository.GetIncludes(false, "Stock", "UseTimeList")
                .Where(c => c.ApproveStatus == EApproveStatus.Delivered
                && !c.IsSpecialCoupon
                && (!c.IsFiexedEffectPeriod || (c.ExpiredDate != null && c.ExpiredDate >= dateNow)));
            var result = queryable.ToArray();
            return result.Select(c => new CouponTemplateDto
            {
                ID = c.ID,
                CouponTypeName = GetEnumDescription(c.CouponType),
                Title = c.Title,
                Validity = c.IsFiexedEffectPeriod ? ((DateTime)c.EffectiveDate).ToString("yyyy-MM-dd") + "~" + ((DateTime)c.ExpiredDate).ToString("yyyy-MM-dd") : "领取后" + (c.EffectiveDaysAfterReceived != null ? c.EffectiveDaysAfterReceived : 0) + "天生效," + (c.EffectiveDays != null ? c.EffectiveDays : 0) + "天有效",
                AproveStatusText = GetEnumDescription(c.ApproveStatus),
                FreeStock = c.Stock.FreeStock,
                //DeliveryStartDate = c.DeliveryStartDate,
                //DeliveryFinishDate = c.DeliveryFinishDate,
                IsFiexedEffectPeriod = c.IsFiexedEffectPeriod,
                ExpiredDate = c.ExpiredDate.ToString(),
                Remark = c.Remark,
                CreatedDate = c.CreatedDate
            }).OrderByDescending(t => t.CreatedDate).ToList();
        }

        private string GetEnumDescription(Enum enumValue)
        {
            string str = enumValue.ToString();
            System.Reflection.FieldInfo field = enumValue.GetType().GetField(str);
            object[] objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (objs == null || objs.Length == 0)
            {
                return str;
            }
            System.ComponentModel.DescriptionAttribute da = (System.ComponentModel.DescriptionAttribute)objs[0];
            return da.Description;
        }

        private string GetMerchantServiceDesc(EMerchantService service)
        {
            var result = string.Empty;
            if ((service & EMerchantService.AllowPets) != 0)
            {
                result += EMerchantService.AllowPets.GetDescription() + ',';
            }
            if ((service & EMerchantService.FreePark) != 0)
            {
                result += EMerchantService.FreePark.GetDescription() + ',';
            }
            if ((service & EMerchantService.FreeWifi) != 0)
            {
                result += EMerchantService.FreeWifi.GetDescription() + ',';
            }
            if ((service & EMerchantService.TakeOut) != 0)
            {
                result += EMerchantService.TakeOut.GetDescription() + ',';
            }
            return result.Length > 0 ? result.Substring(0, result.Length - 1) : result;
        }

        private IEnumerable<string> GetDepartmentInfo(string departmentCodes)
        {
            var result = new List<string>();
            var deptcodelist = departmentCodes != null ? departmentCodes.Split(',').ToList() : new List<string>();
            foreach (var code in deptcodelist)
            {
                var deptname = DepartmentRepo.GetQueryable(false).Where(d => d.Code == code).FirstOrDefault() != null ? DepartmentRepo.GetQueryable(false).Where(d => d.Code == code).FirstOrDefault().Name : string.Empty;
                if (!string.IsNullOrEmpty(deptname))
                {
                    result.Add(deptname);
                }
            }
            return result;
        }

        public CouponTemplateDto GetCouponTemplateDto(Guid id)
        {
            var couponTemplate = Repository.GetIncludes(false, "Stock").FirstOrDefault(f => f.ID == id);
            if (couponTemplate == null) throw new DomainException("未找到记录");
            var couponQueryable = CouponRepo.GetQueryable(false);
            return new CouponTemplateDto
            {
                ID = couponTemplate.ID,
                CouponType = (int)couponTemplate.CouponType,
                CouponTypeName = this.GetEnumDescription(couponTemplate.CouponType),
                TemplateCode = couponTemplate.TemplateCode,
                Title = couponTemplate.Title,
                PutInStartDate = couponTemplate.PutInStartDate.GetValueOrDefault().ToDateString(),
                PutInEndDate = couponTemplate.PutInEndDate.GetValueOrDefault().ToDateString(),
                FreeStock = couponTemplate.Stock.FreeStock,
                Validity = couponTemplate.IsFiexedEffectPeriod ?
                            couponTemplate.EffectiveDate.Value.ToDateString() + "~" + couponTemplate.ExpiredDate.Value.ToDateString()
                            : $"领取后{couponTemplate.EffectiveDaysAfterReceived.GetValueOrDefault()}天生效,{couponTemplate.EffectiveDays.GetValueOrDefault()}天有效",
                CreatedDate = couponTemplate.CreatedDate,
            };
        }

        /// <summary>
        /// 获取推荐会员卡
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductDto> GetRecommendCouponTemplate()
        {
            var result = new List<ProductDto>();
            var temp = new List<CouponTemplate>();
            var queryable = Repository.GetInclude(t => t.Stock, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.Nature == ENature.Card && t.Stock.Stock > 0 && t.ApproveStatus == EApproveStatus.Delivered);
            var recommend = queryable.Where(t => t.IsRecommend).ToList();
            temp = recommend;
            if (temp.Count < 4)
            {
                var additional = queryable.Where(t => !t.IsRecommend).OrderByDescending(t => t.CreatedDate).ToList();
                foreach (var item in additional)
                {
                    temp.Add(item);
                    if (temp.Count >= 4)
                        break;
                }
            }
            else if (temp.Count > 4)
            {
                temp = new List<CouponTemplate>();
                foreach (var item in recommend)
                {
                    temp.Add(item);
                    if (temp.Count >= 4)
                        break;
                }
            }
            foreach (var item in temp)
            {
                result.Add(new ProductDto
                {
                    ID = item.ID,
                    Code = item.TemplateCode,
                    Name = item.Title,
                    ImgUrl = item.CoverImage,
                    Stock = item.Stock.Stock,
                    BasePrice = item.CouponType == ECouponType.Discount ? item.PriceSale : item.CouponValue,
                    PriceSale = item.CouponType == ECouponType.Discount ? item.PriceSale : item.CouponValue,
                    Introduction = item.IntroDetail,
                    DeliveryNotes = "购买后可到会员卡包中查看已有卡券",
                    IsMemberCard = true,
                });
            }
            return result;
        }

        /// <summary>
        /// 获取会员卡
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductDto> GetCardOfCouponTemplate()
        {
            return Repository.GetInclude(t => t.Stock, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.Nature == ENature.Card && t.ApproveStatus == EApproveStatus.Delivered && t.Stock.Stock > 0).ToList()
                .Select(t => new ProductDto
                {
                    ID = t.ID,
                    Code = t.TemplateCode,
                    Name = t.Title,
                    ImgUrl = t.CoverImage,
                    Stock = t.Stock.Stock,
                    BasePrice = t.CouponType == ECouponType.Discount ? t.PriceSale : t.CouponValue,
                    PriceSale = t.CouponType == ECouponType.Discount ? t.PriceSale : t.CouponValue,
                    Introduction = t.IntroDetail,
                    DeliveryNotes = "购买后可到会员卡包中查看已有卡券",
                    IsMemberCard = true,
                });
        }

        /// <summary>
        /// 获取领券中心优惠券
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CouponTemplate> GetCenterCouponTemplate()
        {
            var now = DateTime.Now;
            return Repository.GetQueryable(false).Where(t => t.IsPutaway && t.PutawayTime < now && t.SoldOutTime > now && t.ApproveStatus == EApproveStatus.Delivered && (!t.IsFiexedEffectPeriod || (t.EffectiveDate < now && t.ExpiredDate > now)) && t.PutInStartDate < now && t.PutInEndDate > now && t.IsAvailable && t.Nature == ENature.Coupon).ToList();
        }

        /// <summary>
        /// 获取游戏抽奖优惠券
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CouponTemplate> GetGameCouponTemplate()
        {
            var now = DateTime.Now;
            return Repository.GetQueryable(false).Where(t => t.IsPutaway && t.PutawayTime < now && t.SoldOutTime > now && t.ApproveStatus == EApproveStatus.Delivered && (!t.IsFiexedEffectPeriod || (t.EffectiveDate < now && t.ExpiredDate > now)) && t.PutInStartDate < now && t.PutInEndDate > now && t.IsAvailable && t.Nature == ENature.Coupon).ToList();
        }

        /// <summary>
        /// 更改卡券状态
        /// </summary>
        /// <param name="templateId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool ChangeApproveStatus(Guid templateId, EApproveStatus status)
        {
            var entity = Repository.GetByKey(templateId);
            if (entity == null)
                throw new DomainException("卡券不存在");
            entity.ApproveStatus = status;
            return Repository.Update(entity) > 0;
        }

        /// <summary>
        /// 设置消费返积分比例
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public bool SetConsumePointRate(Guid id, decimal rate)
        {
            if (id == null || rate < 0 || rate > 100)
                throw new DomainException("参数错误");
            var entity = Repository.GetByKey(id);
            if (entity == null)
                throw new DomainException("数据不存在");
            entity.ConsumePointRate = rate;
            return Repository.Update(entity) > 0;
        }

        /// <summary>
        /// 设置股东卡消费返积分比例及折扣系数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="consumePointRate"></param>
        /// <param name="discountRate"></param>
        /// <returns></returns>
        public bool SetConsumePointRateAndDiscountRate(Guid id, decimal consumePointRate, decimal discountRate)
        {
            if (id == null)
                throw new DomainException("参数错误");
            if (consumePointRate < 0 || consumePointRate > 100)
                throw new DomainException("返佣比例参数必须在0到100之间");
            if (discountRate < 0 || discountRate > 100)
                throw new DomainException("折扣系数参数必须在0到100之间");
            var entity = Repository.GetByKey(id);
            if (entity == null)
                throw new DomainException("数据不存在");
            entity.IsStockholderCard = true;
            entity.ConsumePointRate = consumePointRate;
            entity.DiscountRate = discountRate;
            return Repository.Update(entity) > 0;
        }

        /// <summary>
        /// 小程序卡券设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool PutInApplet(Guid id)
        {
            if (id == null)
                throw new DomainException("参数错误");
            var entity = Repository.GetByKey(id);
            entity.IsPutApplet = true;
            return Repository.Update(entity) > 0;
        }
    }
}

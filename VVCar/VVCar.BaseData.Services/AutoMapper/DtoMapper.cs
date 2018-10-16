using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.QueryableExtensions;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Services.DomainServices;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using YEF.Core;

namespace VVCar.BaseData.Services
{
    public static class DtoMapper
    {
        #region ctor.
        static DtoMapper()
        {
            //Initialize();
        }
        #endregion

        #region methods
        public static void Initialize() =>
            //var cfg = new MapperConfigurationExpression();
            //cfg.CreateMap<SysMenu, SysNavMenuDto>()
            //     .ForMember(dest => dest.leaf, opt => opt.MapFrom(src => src.IsLeaf))
            //     .ForMember(dest => dest.text, opt => opt.MapFrom(src => src.Name));
            //Mapper.Initialize(cfg);
            //Mapper.CreateMap<RechargePlan, RechargePlanDto>();
            //Mapper.CreateMap<MemberRegisterDto, Member>();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SysMenu, SysNavMenuDto>()
                .ForMember(dest => dest.leaf, opt => opt.MapFrom(src => src.IsLeaf))
                .ForMember(dest => dest.text, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.iconCls, opt => opt.MapFrom(src => src.SysMenuIcon));

                cfg.CreateMap<ProductCategory, ProductCategoryTreeDto>();
                cfg.CreateMap<MemberGroup, MemberGroupTreeDto>();
                cfg.CreateMap<AgentDepartmentCategory, AgentDepartmentCategoryTreeDto>();

                cfg.CreateMap<CarBitCoinProductCategory, CarBitCoinProductCategoryTreeDto>();

                //VIP Domain
                cfg.CreateMap<Member, IDCodeNameDto>()
                   .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.CardNumber));

                cfg.CreateMap<Member, MemberDto>()
                    //.ForMember(dest => dest.CardType, opt => opt.MapFrom(src => src.Card.CardType.Name))
                    .ForMember(dest => dest.CardStatus, opt => opt.MapFrom(src => src.Card.Status))
                    .ForMember(dest => dest.CardBalance, opt => opt.MapFrom(src => src.Card.CardBalance))
                    .ForMember(dest => dest.EffectiveDate, opt => opt.MapFrom(src => src.Card.EffectiveDate))
                    .ForMember(dest => dest.ExpiredDate, opt => opt.MapFrom(src => src.Card.ExpiredDate))
                    .ForMember(dest => dest.OwnerDepartment, opt => opt.MapFrom(src => src.OwnerDepartment.Name))
                    .ForMember(dest => dest.MemberGroup, opt => opt.MapFrom(src => src.MemberGroup.Name))
                    //.ForMember(dest => dest.MemberGradeName, opt => opt.MapFrom(src => src.MemberGrade.Name))
                    .ForMember(dest => dest.CardType, opt => opt.MapFrom(src => src.Card.CardType))
                    .ForMember(dest => dest.Point, opt => opt.MapFrom(src => src.Point));

                cfg.CreateMap<MemberCardType, MemberCardTypeDto>();

                cfg.CreateMap<Member, MemberLiteInfoDto>()
                    .ForMember(dest => dest.CardStatus, opt => opt.MapFrom(src => src.Card.Status))
                    .ForMember(dest => dest.CardBalance, opt => opt.MapFrom(src => src.Card.CardBalance))
                    //.ForMember(dest => dest.MemberGroup, opt => opt.MapFrom(src => src.OwnerGroup.Name))
                    //.ForMember(dest => dest.MemberGradeName, opt => opt.MapFrom(src => src.MemberGrade.Name))
                    .ForMember(dest => dest.CardTypeID, opt => opt.MapFrom(src => src.Card.CardTypeID));

                cfg.CreateMap<MemberRegisterDto, Member>();

                cfg.CreateMap<ProductCategory, ProductCategoryLiteDto>();

                cfg.CreateMap<CarBitCoinProductCategory, CarBitCoinProductCategoryLiteDto>();

                cfg.CreateMap<Product, ProductLiteDto>();

                cfg.CreateMap<CarBitCoinProduct, CarBitCoinProductLiteDto>();

                cfg.CreateMap<Department, DepartmentLiteDto>();

                cfg.CreateMap<CouponTemplate, CouponFullInfoDto>()
                    .ForMember(dest => dest.TemplateID, opt => opt.MapFrom(src => src.ID))
                    .ForMember(dest => dest.EffectiveDate, opt => opt.Ignore())
                    .ForMember(dest => dest.ExpiredDate, opt => opt.Ignore())
                    .ForMember(dest => dest.CoverImage, opt => opt.MapFrom(src => src.CoverImage))
                    .ForMember(dest => dest.IntroDetail, opt => opt.MapFrom(src => src.IntroDetail))
                    .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock.Stock))
                    .ForMember(dest => dest.UsedStock, opt => opt.MapFrom(src => src.Stock.UsedStock))
                    .ForMember(dest => dest.CollarQuantityLimit, opt => opt.MapFrom(src => src.Stock.CollarQuantityLimit));

                cfg.CreateMap<Coupon, CouponBaseInfoDto>()
                .ForMember(dest => dest.CouponID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Template.Title))
                .ForMember(dest => dest.CouponType, opt => opt.MapFrom(src => src.Template.CouponType))
                .ForMember(dest => dest.CouponValue, opt => opt.MapFrom(src => src.Template.CouponValue))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Template.Color))
                .ForMember(dest => dest.IsMinConsumeLimit, opt => opt.MapFrom(src => src.Template.IsMinConsumeLimit))
                .ForMember(dest => dest.MinConsume, opt => opt.MapFrom(src => src.Template.MinConsume))
                .ForMember(dest => dest.IsExclusive, opt => opt.MapFrom(src => src.Template.IsExclusive))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Template.Stock.Stock))
                .ForMember(dest => dest.UsedStock, opt => opt.MapFrom(src => src.Template.Stock.UsedStock))
                .ForMember(dest => dest.CoverImage, opt => opt.MapFrom(src => src.Template.CoverImage))
                .ForMember(dest => dest.Nature, opt => opt.MapFrom(src => src.Template.Nature));

                cfg.CreateMap<Product, ProductDto>();

                cfg.CreateMap<CarBitCoinProduct, CarBitCoinProductDto>();

                cfg.CreateMap<GameCoupon, GameCouponDto>()
                .ForMember(dest => dest.Nature, opt => opt.MapFrom(src => src.CouponTemplate.Nature))
                .ForMember(dest => dest.CouponType, opt => opt.MapFrom(src => src.CouponTemplate.CouponType))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.CouponTemplate.Title))
                .ForMember(dest => dest.TemplateCode, opt => opt.MapFrom(src => src.CouponTemplate.TemplateCode));

                cfg.CreateMap<StockRecord, StockRecordDto>()
                .ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.Product.Code))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductCategoryName, opt => opt.MapFrom(src => src.Product.ProductCategory.Name));

                cfg.CreateMap<User, UserInfoDto>();

                cfg.CreateMap<CouponPushItem, CouponPushItemDto>()
                .ForMember(dest => dest.TemplateCode, opt => opt.MapFrom(src => src.CouponTemplate.TemplateCode))
                .ForMember(dest => dest.PutInStartDate, opt => opt.MapFrom(src => src.CouponTemplate.PutInStartDate))
                .ForMember(dest => dest.PutInEndDate, opt => opt.MapFrom(src => src.CouponTemplate.PutInEndDate));

                cfg.CreateMap<CouponPushMember, CouponPushMemberDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Member.Name))
                .ForMember(dest => dest.MobilePhoneNo, opt => opt.MapFrom(src => src.Member.MobilePhoneNo));

                cfg.CreateMap<AnnouncementPushMember, AnnouncementPushMemberDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Member.Name))
                .ForMember(dest => dest.MobilePhoneNo, opt => opt.MapFrom(src => src.Member.MobilePhoneNo));

                cfg.CreateMap<GamePushMember, GamePushMemberDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Member.Name))
                .ForMember(dest => dest.MobilePhoneNo, opt => opt.MapFrom(src => src.Member.MobilePhoneNo));

                cfg.CreateMap<CarBitCoinRecord, CarBitCoinRecordDto>()
                .ForMember(dest => dest.CarBitCoinMemberName, opt => opt.MapFrom(src => src.CarBitCoinMember.Name))
                .ForMember(dest => dest.MobilePhoneNo, opt => opt.MapFrom(src => src.CarBitCoinMember.MobilePhoneNo));

                cfg.CreateMap<AgentDepartment, AgentDepartmentDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.MerchantCode, opt => opt.MapFrom(src => src.Merchant.Code))
                .ForMember(dest => dest.MerchantName, opt => opt.MapFrom(src => src.Merchant.Name));

                cfg.CreateMap<AgentDepartmentDto, AgentDepartment>();

                cfg.CreateMap<AgentDepartmentTag, AgentDepartmentTagDto>()
                .ForMember(dest => dest.TagCode, opt => opt.MapFrom(src => src.Tag.Code))
                .ForMember(dest => dest.TagName, opt => opt.MapFrom(src => src.Tag.Name));

                cfg.CreateMap<Tag, TagDto>();

                cfg.CreateMap<SystemSetting, SystemSettingDto>()
                .ForMember(dest => dest.MerchantCode, opt => opt.MapFrom(src => src.Merchant.Code))
                .ForMember(dest => dest.MerchantName, opt => opt.MapFrom(src => src.Merchant.Name));

                cfg.CreateMap<PickUpOrder, PickUpOrderDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Merchant.Name))
                .ForMember(dest => dest.DepartmentAddress, opt => opt.MapFrom(src => src.Merchant.CompanyAddress))
                .ForMember(dest => dest.MobilePhoneNo, opt => opt.MapFrom(src => src.Merchant.MobilePhoneNo));

                cfg.CreateMap<Reimbursement, ReimbursementDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));

                cfg.CreateMap<GamePushItem, GamePushItemDto>()
                .ForMember(dest => dest.GameType, opt => opt.MapFrom(src => src.GameSetting.GameType))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.GameSetting.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.GameSetting.EndTime))
                .ForMember(dest => dest.PeriodDays, opt => opt.MapFrom(src => src.GameSetting.PeriodDays))
                .ForMember(dest => dest.PeriodCounts, opt => opt.MapFrom(src => src.GameSetting.PeriodCounts))
                .ForMember(dest => dest.Limit, opt => opt.MapFrom(src => src.GameSetting.Limit));

                cfg.CreateMap<Order, OrderDto>();

                cfg.CreateMap<TradeHistory, TradeHistoryDto>()
                    .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name))
                    .ForMember(dest => dest.TradeDepartment, opt => opt.MapFrom(src => src.TradeDepartment.Name))
                    .ForMember(dest => dest.TradeSource, opt => opt.MapFrom(src => src.TradeSource))
                    .ForMember(dest => dest.CardRemark, opt => opt.MapFrom(src => src.Card.Remark))
                    .ForMember(dest => dest.CardTypeDesc, opt => opt.MapFrom(src => src.Card.CardType.Name));

                cfg.CreateMap<RechargeHistory, TradeHistoryDto>()
                    .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name))
                    .ForMember(dest => dest.TradeDepartment, opt => opt.MapFrom(src => src.TradeDepartment.Name))
                    .ForMember(dest => dest.TradeSource, opt => opt.MapFrom(src => src.TradeSource))
                    .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType))
                    .ForMember(dest => dest.CardRemark, opt => opt.MapFrom(src => src.Card.Remark))
                    .ForMember(dest => dest.CardTypeDesc, opt => opt.MapFrom(src => src.Card.CardType.Name));

                cfg.CreateMap<CrowdOrder, CrowdOrderDto>()
                .ForMember(dest => dest.CarBitCoinProductName, opt => opt.MapFrom(src => src.CarBitCoinProduct.Name))
                .ForMember(dest => dest.PriceSale, opt => opt.MapFrom(src => src.CarBitCoinProduct.PriceSale))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.CarBitCoinProduct.Stock))
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.CarBitCoinProduct.ImgUrl));

                cfg.CreateMap<MerchantCrowdOrder, MerchantCrowdOrderDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.PriceSale, opt => opt.MapFrom(src => src.Product.PriceSale))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Product.Stock))
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.Product.ImgUrl));

                cfg.CreateMap<MerchantBargainOrder, MerchantBargainOrderDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.PriceSale, opt => opt.MapFrom(src => src.Product.PriceSale))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Product.Stock))
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.Product.ImgUrl));

                cfg.CreateMap<CrowdOrderRecord, CrowdOrderRecordDto>()
                .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.CrowdOrder.CarBitCoinProductID))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.CrowdOrder.CarBitCoinProduct.Name))
                .ForMember(dest => dest.PriceSale, opt => opt.MapFrom(src => src.CrowdOrder.CarBitCoinProduct.PriceSale))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.CrowdOrder.CarBitCoinProduct.Stock))
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.CrowdOrder.CarBitCoinProduct.ImgUrl))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CrowdOrder.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.CrowdOrder.Price))
                .ForMember(dest => dest.PeopleCount, opt => opt.MapFrom(src => src.CrowdOrder.PeopleCount))
                .ForMember(dest => dest.CarBitCoinProduct, opt => opt.MapFrom(src => src.CrowdOrder.CarBitCoinProduct));

                cfg.CreateMap<MerchantCrowdOrderRecord, MerchantCrowdOrderRecordDto>()
                .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.MerchantCrowdOrder.ProductID))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.MerchantCrowdOrder.Product.Name))
                .ForMember(dest => dest.PriceSale, opt => opt.MapFrom(src => src.MerchantCrowdOrder.Product.PriceSale))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.MerchantCrowdOrder.Product.Stock))
                .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.MerchantCrowdOrder.Product.ImgUrl))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.MerchantCrowdOrder.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.MerchantCrowdOrder.Price))
                .ForMember(dest => dest.PeopleCount, opt => opt.MapFrom(src => src.MerchantCrowdOrder.PeopleCount))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.MerchantCrowdOrder.Product));

                cfg.CreateMap<MerchantBargainOrderRecord, MerchantBargainOrderRecordDto>()
               .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.MerchantBargainOrder.ProductID))
               .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.MerchantBargainOrder.Product.Name))
               .ForMember(dest => dest.PriceSale, opt => opt.MapFrom(src => src.MerchantBargainOrder.Product.PriceSale))
               .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.MerchantBargainOrder.Product.Stock))
               .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom(src => src.MerchantBargainOrder.Product.ImgUrl))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.MerchantBargainOrder.Name))
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.MerchantBargainOrder.Price))
               .ForMember(dest => dest.PeopleCount, opt => opt.MapFrom(src => src.MerchantBargainOrder.PeopleCount))
               .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.MerchantBargainOrder.Product));

                cfg.CreateMap<MerchantBargainOrderRecordItem, MerchantBargainOrderRecordItemDto>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name));

                cfg.CreateMap<UnsaleProductSettingItem, UnsaleProductSettingItemDto>()
                .ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.Product.Code))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

                cfg.CreateMap<StockholderDividend, StockholderDividendDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Member.Name));

                cfg.CreateMap<AdvisementBrowseHistory, AdvisementBrowseHistoryDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.AdvisementSetting.Title));
            });//Mapper.CreateMap<Member, MemberDto>()//    //.ForMember(dest => dest.CardType, opt => opt.MapFrom(src => src.Card.CardType.Name))//    .ForMember(dest => dest.CardStatus, opt => opt.MapFrom(src => src.Card.Status))//    .ForMember(dest => dest.CardBalance, opt => opt.MapFrom(src => src.Card.CardBalance))//    .ForMember(dest => dest.EffectiveDate, opt => opt.MapFrom(src => src.Card.EffectiveDate))//    .ForMember(dest => dest.ExpiredDate, opt => opt.MapFrom(src => src.Card.ExpiredDate))//    .ForMember(dest => dest.OwnerDepartment, opt => opt.MapFrom(src => src.OwnerDepartment.Name))//    .ForMember(dest => dest.MemberGroup, opt => opt.MapFrom(src => src.OwnerGroup.Name))//    .ForMember(dest => dest.MemberGradeName, opt => opt.MapFrom(src => src.MemberGrade.Name))//    .ForMember(dest => dest.CardType, opt => opt.MapFrom(src => src.Card.CardType))//    .ForMember(dest => dest.Point, opt => opt.MapFrom(src => src.Point));//Mapper.CreateMap<Member, MemberLiteInfoDto>()//    .ForMember(dest => dest.CardStatus, opt => opt.MapFrom(src => src.Card.Status))//    .ForMember(dest => dest.CardBalance, opt => opt.MapFrom(src => src.Card.CardBalance))//    .ForMember(dest => dest.MemberGroup, opt => opt.MapFrom(src => src.OwnerGroup.Name))//    .ForMember(dest => dest.MemberGradeName, opt => opt.MapFrom(src => src.MemberGrade.Name))//    .ForMember(dest => dest.CardTypeID, opt => opt.MapFrom(src => src.Card.CardTypeID));//Mapper.CreateMap<Member, MemberNanoInfoDto>();//Mapper.CreateMap<MemberCardType, MemberCardTypeDto>();//Mapper.CreateMap<Department, DepartmentLiteDto>();//Mapper.CreateMap<MemberCard, MemberCardExportDto>();//Mapper.CreateMap<ChatUserModel, WeChatFans>()//    .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.city))//    .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.country))//    .ForMember(dest => dest.HeadImgUrl, opt => opt.MapFrom(src => src.headimgurl))//    .ForMember(dest => dest.NickName, opt => opt.MapFrom(src => src.nickname))//    .ForMember(dest => dest.OpenID, opt => opt.MapFrom(src => src.openid))//    .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.province))//    .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.sex))//    .ForMember(dest => dest.SubscribeTime, opt => opt.MapFrom(src => src.SubscribeTime));//Mapper.CreateMap<WeChatFans, WeChatFansDto>();//Mapper.CreateMap<NewUpdateRechargePlanDto, RechargePlan>();//Mapper.CreateMap<MemberCard, MemberCardThemeDto>();//Mapper.CreateMap<CardThemeGroup, CardThemeGroupDto>()//    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CardThemeCategory.Name))//    .ForMember(dest => dest.CardThemeCategoryID, opt => opt.MapFrom(src => src.CardThemeCategory.ID))//    .ForMember(dest => dest.CategoryGrade, opt => opt.MapFrom(src => src.CardThemeCategory.Grade));

        /// <summary>
        /// 映射到目标类型
        /// 在调用此方法前必须配置映射关系
        /// </summary>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source"></param>
        public static TDestination MapTo<TDestination>(this object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// 映射到目标
        /// 在调用此方法前必须配置映射关系
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TDestination">Destination type</typeparam>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// 映射到目标
        /// </summary>
        /// <typeparam name="TResult">Destination type</typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IQueryable<TResult> MapTo<TResult>(this IQueryable source)
        {
            return source.ProjectTo<TResult>();
        }

        /// <summary>
        /// 映射到目标
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TResult">Destination type</typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IQueryable<TResult> MapTo<TSource, TResult>(this IQueryable<TSource> source)
        {
            return source.ProjectTo<TResult>();
        }

        #endregion

    }
}

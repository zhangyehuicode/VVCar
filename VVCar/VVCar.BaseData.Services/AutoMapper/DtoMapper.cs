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
        public static void Initialize()
        {
            //var cfg = new MapperConfigurationExpression();
            //cfg.CreateMap<SysMenu, SysNavMenuDto>()
            //     .ForMember(dest => dest.leaf, opt => opt.MapFrom(src => src.IsLeaf))
            //     .ForMember(dest => dest.text, opt => opt.MapFrom(src => src.Name));
            //Mapper.Initialize(cfg);
            //Mapper.Initialize(t => t.CreateMap<TradeHistory, TradeHistoryDto>()
            //    .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name))
            //    .ForMember(dest => dest.TradeDepartment, opt => opt.MapFrom(src => src.TradeDepartment.Name))
            //    .ForMember(dest => dest.TradeSource, opt => opt.MapFrom(src => src.TradeSource))
            //    .ForMember(dest => dest.CardRemark, opt => opt.MapFrom(src => src.Card.Remark))
            //    .ForMember(dest => dest.CardTypeDesc, opt => opt.MapFrom(src => src.Card.CardType.Name)));
            //Mapper.CreateMap<RechargeHistory, TradeHistoryDto>()
            //    .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name))
            //    .ForMember(dest => dest.TradeDepartment, opt => opt.MapFrom(src => src.TradeDepartment.Name))
            //    .ForMember(dest => dest.TradeSource, opt => opt.MapFrom(src => src.TradeSource))
            //    .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType))
            //    .ForMember(dest => dest.CardRemark, opt => opt.MapFrom(src => src.Card.Remark))
            //    .ForMember(dest => dest.CardTypeDesc, opt => opt.MapFrom(src => src.Card.CardType.Name));
            //Mapper.CreateMap<RechargePlan, RechargePlanDto>();
            //Mapper.CreateMap<MemberRegisterDto, Member>();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SysMenu, SysNavMenuDto>()
                .ForMember(dest => dest.leaf, opt => opt.MapFrom(src => src.IsLeaf))
                .ForMember(dest => dest.text, opt => opt.MapFrom(src => src.Name));

                cfg.CreateMap<ProductCategory, ProductCategoryTreeDto>();

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
                    //.ForMember(dest => dest.MemberGroup, opt => opt.MapFrom(src => src.OwnerGroup.Name))
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
                cfg.CreateMap<Product, ProductLiteDto>();

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
            });
            //Mapper.CreateMap<Member, MemberDto>()
            //    //.ForMember(dest => dest.CardType, opt => opt.MapFrom(src => src.Card.CardType.Name))
            //    .ForMember(dest => dest.CardStatus, opt => opt.MapFrom(src => src.Card.Status))
            //    .ForMember(dest => dest.CardBalance, opt => opt.MapFrom(src => src.Card.CardBalance))
            //    .ForMember(dest => dest.EffectiveDate, opt => opt.MapFrom(src => src.Card.EffectiveDate))
            //    .ForMember(dest => dest.ExpiredDate, opt => opt.MapFrom(src => src.Card.ExpiredDate))
            //    .ForMember(dest => dest.OwnerDepartment, opt => opt.MapFrom(src => src.OwnerDepartment.Name))
            //    .ForMember(dest => dest.MemberGroup, opt => opt.MapFrom(src => src.OwnerGroup.Name))
            //    .ForMember(dest => dest.MemberGradeName, opt => opt.MapFrom(src => src.MemberGrade.Name))
            //    .ForMember(dest => dest.CardType, opt => opt.MapFrom(src => src.Card.CardType))
            //    .ForMember(dest => dest.Point, opt => opt.MapFrom(src => src.Point));
            //Mapper.CreateMap<Member, MemberLiteInfoDto>()
            //    .ForMember(dest => dest.CardStatus, opt => opt.MapFrom(src => src.Card.Status))
            //    .ForMember(dest => dest.CardBalance, opt => opt.MapFrom(src => src.Card.CardBalance))
            //    .ForMember(dest => dest.MemberGroup, opt => opt.MapFrom(src => src.OwnerGroup.Name))
            //    .ForMember(dest => dest.MemberGradeName, opt => opt.MapFrom(src => src.MemberGrade.Name))
            //    .ForMember(dest => dest.CardTypeID, opt => opt.MapFrom(src => src.Card.CardTypeID));
            //Mapper.CreateMap<Member, MemberNanoInfoDto>();

            //Mapper.CreateMap<MemberCardType, MemberCardTypeDto>();
            //Mapper.CreateMap<Department, DepartmentLiteDto>();
            //Mapper.CreateMap<MemberCard, MemberCardExportDto>();
            //Mapper.CreateMap<ChatUserModel, WeChatFans>()
            //    .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.city))
            //    .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.country))
            //    .ForMember(dest => dest.HeadImgUrl, opt => opt.MapFrom(src => src.headimgurl))
            //    .ForMember(dest => dest.NickName, opt => opt.MapFrom(src => src.nickname))
            //    .ForMember(dest => dest.OpenID, opt => opt.MapFrom(src => src.openid))
            //    .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.province))
            //    .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.sex))
            //    .ForMember(dest => dest.SubscribeTime, opt => opt.MapFrom(src => src.SubscribeTime));
            //Mapper.CreateMap<WeChatFans, WeChatFansDto>();
            //Mapper.CreateMap<NewUpdateRechargePlanDto, RechargePlan>();
            //Mapper.CreateMap<MemberCard, MemberCardThemeDto>();
            //Mapper.CreateMap<CardThemeGroup, CardThemeGroupDto>()
            //    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CardThemeCategory.Name))
            //    .ForMember(dest => dest.CardThemeCategoryID, opt => opt.MapFrom(src => src.CardThemeCategory.ID))
            //    .ForMember(dest => dest.CategoryGrade, opt => opt.MapFrom(src => src.CardThemeCategory.Grade));
        }

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

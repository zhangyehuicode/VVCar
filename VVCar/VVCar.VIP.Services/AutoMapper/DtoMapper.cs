using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using YEF.Core;

namespace VVCar.VIP.Services
{
    public static class DtoMapper
    {
        #region ctor.

        static DtoMapper()
        {
            Initialize();
        }

        #endregion

        #region methods

        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
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
            });
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

        #endregion
    }
}

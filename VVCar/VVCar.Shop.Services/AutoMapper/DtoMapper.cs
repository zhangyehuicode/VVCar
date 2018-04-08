using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Services
{
    //public static class DtoMapper
    //{
    //    #region ctor.

    //    static DtoMapper()
    //    {
    //        Initialize();
    //    }

    //    #endregion

    //    #region methods

    //    public static void Initialize()
    //    {
    //        //Mapper.Initialize(cfg =>
    //        //{
    //        //    cfg.CreateMap<ProductCategory, ProductCategoryTreeDto>();
    //        //});
    //    }

    //    /// <summary>
    //    /// 映射到目标类型
    //    /// 在调用此方法前必须配置映射关系
    //    /// </summary>
    //    /// <typeparam name="TDestination">目标类型</typeparam>
    //    /// <param name="source"></param>
    //    public static TDestination MapTo<TDestination>(this object source)
    //    {
    //        return Mapper.Map<TDestination>(source);
    //    }

    //    /// <summary>
    //    /// 映射到目标
    //    /// 在调用此方法前必须配置映射关系
    //    /// </summary>
    //    /// <typeparam name="TSource">Source type</typeparam>
    //    /// <typeparam name="TDestination">Destination type</typeparam>
    //    /// <param name="source">Source object</param>
    //    /// <param name="destination">Destination object</param>
    //    /// <returns></returns>
    //    public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
    //    {
    //        return Mapper.Map(source, destination);
    //    }

    //    /// <summary>
    //    /// 映射到目标
    //    /// </summary>
    //    /// <typeparam name="TResult">Destination type</typeparam>
    //    /// <param name="source"></param>
    //    /// <returns></returns>
    //    public static IQueryable<TResult> MapTo<TResult>(this IQueryable source)
    //    {
    //        return source.ProjectTo<TResult>();
    //    }

    //    /// <summary>
    //    /// 映射到目标
    //    /// </summary>
    //    /// <typeparam name="TSource">Source type</typeparam>
    //    /// <typeparam name="TResult">Destination type</typeparam>
    //    /// <param name="source"></param>
    //    /// <returns></returns>
    //    public static IQueryable<TResult> MapTo<TSource, TResult>(this IQueryable<TSource> source)
    //    {
    //        return source.ProjectTo<TResult>();
    //    }

    //    #endregion
    //}
}

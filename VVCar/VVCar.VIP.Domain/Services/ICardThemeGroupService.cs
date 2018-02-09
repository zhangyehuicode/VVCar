using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 卡片主题分组 领域服务接口
    /// </summary>
    public interface ICardThemeGroupService : IDomainService<IRepository<CardThemeGroup>, CardThemeGroup, Guid>
    {
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns></returns>
        IEnumerable<CardThemeGroupDto> Search(CardThemeGroupFilter filter, ref int totalCount);

        /// <summary>
        /// 查时间段
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<CardThemeGroupUseTime> SearchTime(CardThemeGroupUseTime param);

        /// <summary>
        /// 查时图片
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<MemberCardTheme> SearchImg(MemberCardTheme param);

        /// <summary>
        /// 查时week
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<CardThemeGroupDto> Searchwek(SearchCardThemeGroupFilter filter, ref int totalCount);

        /// <summary>
        /// 查询适用时间段
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<CardThemeGroupUseTime> SearchUseTime(SearchCardThemeGroupFilter filter, ref int totalCount);

        /// <summary>
        /// 升序
        /// </summary>
        bool setIndex(CardThemeGroup param);

        /// <summary>
        /// 下降序
        /// </summary>
        bool UpIndex(CardThemeGroup param);

        /// <summary>
        /// 启用
        /// </summary>
        bool setAvailable(CardThemeGroup param);

        /// <summary>
        /// 禁用
        /// </summary>
        bool UpAvailable(CardThemeGroup param);

        /// <summary>
        /// 下降序
        /// </summary>
        bool DeleteGiftCardTem(CardThemeGroup param);

        /// <summary>
        /// 通过卡号找信息
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        MemberCardDto GetCardInfoByNumber(string number);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 寻客侠广告设置
    /// </summary>
    public class AdvisementSettingService : DomainServiceBase<IRepository<AdvisementSetting>, AdvisementSetting, Guid>, IAdvisementSettingService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AdvisementSettingService()
        {
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override AdvisementSetting Add(AdvisementSetting entity)
        {
            if (entity == null)
                throw new DomainException("参数错误");
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(AdvisementSetting entity)
        {
            if (entity == null)
                return false;
            var advisementSetting = Repository.GetByKey(entity.ID);
            if (advisementSetting == null)
                return false;
            if (advisementSetting.CreatedUserID != AppContext.CurrentSession.UserID)
                throw new DomainException("不能修改其他人的广告设计!");
            advisementSetting.Title = entity.Title;
            advisementSetting.SubTitle = entity.SubTitle;
            advisementSetting.ImgUrl = entity.ImgUrl;
            advisementSetting.Content = entity.Content;
            advisementSetting.LastUpdateDate = DateTime.Now;
            advisementSetting.LastUpdateUserID = AppContext.CurrentSession.UserID;
            advisementSetting.LastUpdateUser = AppContext.CurrentSession.UserName;
            return Repository.Update(advisementSetting) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override bool Delete(Guid key)
        {
            if (key == null)
                return false;
            var advisementSetting = Repository.GetByKey(key);
            if (advisementSetting == null)
                return false;
            advisementSetting.IsDeleted = true;
            advisementSetting.LastUpdateDate = DateTime.Now;
            advisementSetting.LastUpdateUserID = AppContext.CurrentSession.UserID;
            advisementSetting.LastUpdateUser = AppContext.CurrentSession.UserName;
            return Repository.Update(advisementSetting) > 0;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool BatchDelete(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数错误");
            var advisementSettingList = Repository.GetQueryable(true).Where(t => ids.Contains(t.ID)).ToList();
            if (advisementSettingList == null || advisementSettingList.Count() < 1)
                throw new DomainException("数据不存在");
            advisementSettingList.ForEach(t => {
                if (t.CreatedUserID == AppContext.CurrentSession.UserID)
                {
                    t.IsDeleted = true;
                    t.LastUpdateDate = DateTime.Now;
                    t.LastUpdateUserID = AppContext.CurrentSession.UserID;
                    t.LastUpdateUser = AppContext.CurrentSession.UserName;
                }
                else
                {
                    throw new DomainException("不能删除不是自己创建的数据");
                }
            });
            return Repository.UpdateRange(advisementSettingList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<AdvisementSettingDto> Search(AdvisementSettingFilter filter, out int totalCount)
        {
            var queryable = Repository.GetInclude(t => t.AdvisementBrowseHistoryList, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.ID.HasValue)
                queryable = queryable.Where(t => t.ID == filter.ID);
            if (!string.IsNullOrEmpty(filter.Title))
                queryable = queryable.Where(t => t.Title.Contains(filter.Title));
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            var result = new List<AdvisementSettingDto>();
            var queryList = queryable.ToList();
            var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            queryList.ForEach(t =>
            {
                var advisementSettingDto = new AdvisementSettingDto {
                    ID = t.ID,
                    Title = t.Title,
                    SubTitle = t.SubTitle,
                    Content = t.Content,
                    ImgUrl = t.ImgUrl,
                    CreatedUser = t.CreatedUser,
                    CreatedDate = t.CreatedDate,
                    FocusTodayCount = t.AdvisementBrowseHistoryList.Where(c => c.CreatedDate > start).Select(s => s.NickName).Distinct().Count(),
                    TotalFocusCount = t.AdvisementBrowseHistoryList.Select(s => s.NickName).Distinct().Count(),
                };
                result.Add(advisementSettingDto);
            });
            return result;
        }
    }
}

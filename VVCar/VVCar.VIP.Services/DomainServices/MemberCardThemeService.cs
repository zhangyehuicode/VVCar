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
    /// 卡片类型主题 领域服务接口
    /// </summary>
    public class MemberCardThemeService : DomainServiceBase<IRepository<MemberCardTheme>, MemberCardTheme, Guid>, IMemberCardThemeService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberCardThemeService"/> class.
        /// </summary>
        public MemberCardThemeService()
        {
        }

        protected override bool DoValidate(MemberCardTheme entity)
        {
            if (entity.CardThemeGroupID.HasValue)
            {
                var count = Repository.GetQueryable(false).Where(t => t.CardThemeGroupID == entity.CardThemeGroupID.Value && t.ID != entity.ID).Count();
                if (count > 11)
                    throw new DomainException("一个分组最多可以有12个主题");
            }
            return base.DoValidate(entity);
        }

        public override MemberCardTheme Add(MemberCardTheme entity)
        {
            if (entity == null)
                return null;

            //if (entity.IsDefault)
            //{
            //    var exists = Repository.Exists(t => t.IsDefault && t.CardThemeGroupID == entity.CardThemeGroupID && t.ID != entity.ID);
            //    if (exists)
            //        throw new DomainException("一个分组只能有一个默认主题");
            //}
            //else
            //{
            //    var exists = Repository.Exists(t => t.IsDefault && t.CardThemeGroupID == entity.CardThemeGroupID && t.ID != entity.ID);
            //    if (!exists)
            //        throw new DomainException("一个分组必须有一个默认主题");
            //}

            entity.ID = Util.NewID();
            var index = 1;
            var theme = Repository.GetQueryable(false).Where(t => t.CardTypeID == entity.CardTypeID).OrderByDescending(t => t.Index).FirstOrDefault();
            if (theme != null)
                index = theme.Index + 1;
            entity.Index = index;
            entity.IsAvailable = true;
            return base.Add(entity);
        }

        public bool SetAvailable(MemberCardThemeSetAvailableDto settingInfo)
        {
            if (settingInfo == null)
                return false;
            var theme = Repository.GetByKey(settingInfo.ThemeID);
            if (theme == null)
                return false;
            if (theme.IsDefault && !settingInfo.IsAvailable)
                throw new DomainException("默认主题不能禁用");
            theme.IsAvailable = settingInfo.IsAvailable;
            return base.Update(theme);
        }

        public override bool Update(MemberCardTheme entity)
        {
            if (entity == null)
                return false;
            var theme = Repository.GetByKey(entity.ID);
            if (theme == null)
                return false;
            try
            {
                UnitOfWork.BeginTransaction();
                var defaultTheme = Repository.GetQueryable().Where(t => t.IsDefault && t.ID != entity.ID).FirstOrDefault();
                //if (defaultTheme == null && !entity.IsDefault)
                //    throw new DomainException("必须有一个默认主题");
                //else if (defaultTheme != null && entity.IsDefault)
                //{
                //    defaultTheme.IsDefault = false;
                //    var updateDefaultThemeRes = base.Update(defaultTheme);
                //    if (!updateDefaultThemeRes)
                //        throw new DomainException("替换原有默认主题失败");
                //}
                theme.Name = entity.Name;
                theme.ImgUrl = entity.ImgUrl;
                theme.IsDefault = entity.IsDefault;
                theme.CardThemeGroupID = entity.CardThemeGroupID;
                var result = base.Update(theme);
                UnitOfWork.CommitTransaction();
                return result;
            }
            catch (Exception ex)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException(ex.Message);
            }
        }

        public override bool Delete(Guid key)
        {
            var theme = Repository.GetByKey(key);
            if (theme == null)
                return false;
            //if (theme.IsDefault)
            //    throw new DomainException("默认主题不能删除");
            return base.Delete(key);
        }

        public IEnumerable<MemberCardTheme> Query(MemberCardThemeFilter filter, ref int totalCount)
        {
            var queryable = Repository.GetIncludes(false, "CardThemeGroup", "CardThemeGroup.CardThemeCategory");
            if (filter.CardTypeID.HasValue)
                queryable = queryable.Where(t => t.CardTypeID == filter.CardTypeID.Value);
            if (!string.IsNullOrEmpty(filter.Name))
                queryable = queryable.Where(t => t.Name.Contains(filter.Name));
            //if (filter.IsDefault.HasValue)
            //    queryable = queryable.Where(t => t.IsDefault == filter.IsDefault.Value);
            //if (filter.IsAvailable.HasValue)
            //    queryable = queryable.Where(t => t.IsAvailable);
            if (filter.CardThemeGroupID.HasValue)
                queryable = queryable.Where(t => t.CardThemeGroupID == filter.CardThemeGroupID.Value);
            //queryable = queryable.Where(t => t.CardThemeGroupID == filter.CardThemeCategoryID.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderBy(t => t.CardThemeGroup.CardThemeCategory.Grade).OrderBy(t => t.CardThemeGroup.Index).OrderBy(t => t.Index).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.OrderBy(t => t.CardThemeGroup.CardThemeCategory.Grade).OrderBy(t => t.CardThemeGroup.Index).OrderBy(t => t.Index).ToArray();
        }

        public bool SetIndex(MemberCardThemeSetIndexDto adjustInfo)
        {
            if (adjustInfo == null || adjustInfo.CardThemeID == null)
                throw new DomainException("参数错误");
            var theme = Repository.GetQueryable().Where(t => t.ID == adjustInfo.CardThemeID).FirstOrDefault();
            if (theme == null)
                throw new DomainException("未找到对应的主题");
            var themes = Repository.GetQueryable().Where(t => t.CardTypeID == theme.CardTypeID).OrderBy(t => t.Index).ToArray();
            if (themes.Count() == 1)
                return true;
            var indexs = themes.Select(t => t.Index);
            if ((adjustInfo.Direction == 0 && theme.Index == indexs.Max()) || (adjustInfo.Direction == 1 && theme.Index == indexs.Min()))
                return true;
            var modify = false;
            themes.ForEach(t =>
            {
                if (adjustInfo.Direction == 1)
                {
                    if (t.Index + 1 == theme.Index && !modify)
                    {
                        t.Index = theme.Index;
                        theme.Index -= 1;
                        modify = true;
                    }
                }
                else
                {
                    if (t.Index - 1 == theme.Index && !modify)
                    {
                        t.Index = theme.Index;
                        theme.Index += 1;
                        modify = true;
                    }
                }
            });
            UnitOfWork.BeginTransaction();
            try
            {
                Repository.Update(theme);
                Repository.Update(themes);
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException(e.Message);
            }
        }
    }
}

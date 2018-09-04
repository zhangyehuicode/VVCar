using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.BaseData.Services.DomainServices
{
    /// <summary>
    /// 系统参数设置领域服务
    /// </summary>
    public partial class SystemSettingService : DomainServiceBase<IRepository<SystemSetting>, SystemSetting, Guid>, ISystemSettingService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemSettingService"/> class.
        /// </summary>
        public SystemSettingService()
        {
        }

        #region methods

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override SystemSetting Add(SystemSetting entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.Index = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.Type == entity.Type).Select(t => t.Index).Max() + 1;
            entity.MerchantID = entity.MerchantID;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override bool Delete(Guid key)
        {
            var entity = Repository.Get(p => p.ID == key);
            if (entity == null)
                throw new DomainException("数据不存或已删除");
            entity.IsDeleted = true;
            entity.LastUpdateDate = DateTime.Now;
            entity.LastUpdateUserID = AppContext.CurrentSession.UserID;
            entity.LastUpdateUser = AppContext.CurrentSession.UserName;
            return base.Update(entity);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(SystemSetting entity)
        {
            if (entity == null)
                return false;
            return UpdateSetting(entity.ID, entity.Caption, entity.Name, entity.TemplateName, entity.SettingValue);
        }

        #endregion

        #region ISystemSettingService 成员


        /// <summary>
        /// 更新模板编号和模板值
        /// </summary>
        /// <param name="settingID"></param>
        /// <param name="caption"></param>
        /// <param name="name"></param>
        /// <param name="templateName"></param>
        /// <param name="settingValue"></param>
        /// <returns></returns>
        public bool UpdateSetting(Guid settingID, string caption, string name, string templateName, string settingValue)
        {
            var setting = this.Repository.GetByKey(settingID);
            if (setting == null)
                throw new DomainException("修改失败，记录不存在。");
            setting.Name = name;
            setting.Caption = caption;
            setting.TemplateName = templateName;
            setting.SettingValue = settingValue;
            setting.LastUpdateUserID = AppContext.CurrentSession.UserID;
            setting.LastUpdateUser = AppContext.CurrentSession.UserName;
            setting.LastUpdateDate = DateTime.Now;
            Repository.Update(setting);
            return true;
        }

        public string GetSettingValue(string name, Guid? merchantId = null)
        {
            if (merchantId == null)
                merchantId = AppContext.CurrentSession.MerchantID;
            var setting = Repository.GetQueryable(false).Where(t => t.MerchantID == merchantId)
                .Where(t => t.Name == name).Select(t => new
                {
                    t.SettingValue,
                    t.DefaultValue
                }).FirstOrDefault();
            if (setting == null)
                return null;
            return setting.SettingValue == null ? setting.DefaultValue : setting.SettingValue;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<SystemSettingDto> Search(SystemSettingFilter filter, out int totalCount)
        {
            var queryable = Repository.GetInclude(t=>t.Merchant, false).Where(t => t.IsAvailable && t.IsVisible);
            if (AppContext.CurrentSession.MerchantID != Guid.Parse("00000000-0000-0000-0000-000000000001"))
                queryable = queryable.Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                    queryable = queryable.Where(t => t.Name == filter.Name);
            }
            if (!string.IsNullOrEmpty(filter.MerchantCode))
                queryable = queryable.Where(t => t.Merchant.Code.Contains(filter.MerchantCode));
            if (!string.IsNullOrEmpty(filter.MerchantName))
                queryable = queryable.Where(t => t.Merchant.Name.Contains(filter.MerchantName));
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            var systemSettingDtoList = queryable.OrderBy(t=>t.Caption).MapTo<SystemSettingDto>().ToList();
            systemSettingDtoList.ForEach(s => s.SettingValue = s.SettingValue ?? s.DefaultValue);
            return systemSettingDtoList;
        }

        #endregion
    }
}

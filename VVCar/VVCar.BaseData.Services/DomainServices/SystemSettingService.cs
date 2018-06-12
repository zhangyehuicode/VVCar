using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public override SystemSetting Add(SystemSetting entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        public override bool Delete(Guid key)
        {
            var entity = Repository.Get(p => p.ID == key);
            if (entity == null)
                throw new DomainException("数据不存或已删除");
            entity.IsDeleted = true;
            return base.Update(entity);
        }

        public override bool Update(SystemSetting entity)
        {
            if (entity == null)
                return false;
            return UpdateSetting(entity.ID, entity.SettingValue);
        }

        #endregion

        #region ISystemSettingService 成员

        public bool UpdateSetting(Guid settingID, string settingValue)
        {
            var setting = this.Repository.GetByKey(settingID);
            if (setting == null)
                throw new DomainException("修改失败，记录不存在。");
            setting.SettingValue = settingValue;
            setting.LastUpdateUserID = AppContext.CurrentSession.UserID;
            setting.LastUpdateUser = AppContext.CurrentSession.UserName;
            setting.LastUpdateDate = DateTime.Now;
            Repository.Update(setting);
            return true;
        }

        public string GetSettingValue(string name)
        {
            var setting = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID)
                .Where(t => t.Name == name).Select(t => new
                {
                    t.SettingValue,
                    t.DefaultValue
                }).FirstOrDefault();
            if (setting == null)
                return null;
            return setting.SettingValue == null ? setting.DefaultValue : setting.SettingValue;
        }

        public IEnumerable<SystemSetting> Search(SystemSettingFilter filter)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.IsAvailable && t.IsVisible && t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                    queryable = queryable.Where(t => t.Name == filter.Name);
            }
            var queryData = queryable.OrderBy(t => t.Caption).ToList();
            queryData.ForEach(s => s.SettingValue = s.SettingValue ?? s.DefaultValue);
            return queryData;
        }

        #endregion
    }
}

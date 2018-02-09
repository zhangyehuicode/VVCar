using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.BaseData.Services.DomainServices
{
    public partial class DataDictValueService : DomainServiceBase<IRepository<DataDictValue>, DataDictValue, Guid>, IDataDictValueService
    {
        public DataDictValueService()
        {
        }

        #region methods

        public override DataDictValue Add(DataDictValue entity)
        {
            entity.ID = Util.NewID();
            return base.Add(entity);
        }

        public override bool Delete(Guid key)
        {
            var dictValue = this.Repository.GetByKey(key);
            if (dictValue == null)
                throw new DomainException("删除失败，数据不存在！");
            dictValue.IsAvailable = false;
            return this.Repository.Update(dictValue) > 0;
        }

        public override bool Update(DataDictValue entity)
        {
            var dictValue = this.Repository.GetByKey(entity.ID);
            if (dictValue == null)
                throw new DomainException("更新失败，数据不存在！");
            dictValue.DictValue = entity.DictValue;
            dictValue.DictName = entity.DictName;
            dictValue.IsAvailable = entity.IsAvailable;
            dictValue.Index = entity.Index;
            return base.Update(dictValue);
        }

        protected override bool DoValidate(DataDictValue entity)
        {
            if (entity == null)
                return false;
            bool exists = this.Repository.Exists(t => t.DictType == entity.DictType && t.DictValue == entity.DictValue && t.ID != entity.ID);
            if (exists)
                throw new DomainException(String.Format("字典值 {0} 已使用，不能重复添加。", entity.DictValue));
            exists = this.Repository.Exists(t => t.DictType == entity.DictType && t.DictName == entity.DictName && t.ID != entity.ID);
            if (exists)
                throw new DomainException(String.Format("字典名称 {0} 已使用，不能重复添加。", entity.DictName));
            exists = this.Repository.Exists(t => t.DictType == entity.DictType && t.Index == entity.Index && t.ID != entity.ID);
            if (exists)
                throw new DomainException(String.Format("序号 {0} 已使用，不能重复添加。", entity.Index));
            return true;
        }

        #endregion

        #region IDataDictService 成员

        public IEnumerable<DataDictValue> GetDictValuesByType(string dictType)
        {
            if (string.IsNullOrEmpty(dictType))
                return null;
            return this.Repository.GetQueryable(false).Where(t => t.DictType == dictType && t.IsAvailable == true).OrderBy(t => t.Index).ToArray();
        }

        #endregion
    }
}

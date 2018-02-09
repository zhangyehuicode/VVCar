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
    public partial class DataDictTypeService : DomainServiceBase<IRepository<DataDictType>, DataDictType, Guid>, IDataDictTypeService
    {
        public DataDictTypeService()
        {
        }

        #region methods

        public override DataDictType Add(DataDictType entity)
        {
            entity.ID = Util.NewID();
            return base.Add(entity);
        }

        public override bool Delete(Guid key)
        {
            return false;
        }

        public override bool Update(DataDictType entity)
        {
            var dictType = this.Repository.GetByKey(entity.ID);
            if (dictType == null)
                throw new DomainException("更新失败，数据不存在！");
            dictType.Name = entity.Name;
            dictType.Index = entity.Index;
            return base.Update(entity);
        }

        protected override bool DoValidate(DataDictType entity)
        {
            if (entity == null)
                return false;
            bool exists = this.Repository.Exists(t => t.Code == entity.Code && t.ID != entity.ID);
            if (exists)
                throw new DomainException(String.Format("代码 {0} 已使用，不能重复添加。", entity.Code));
            exists = this.Repository.Exists(t => t.Name == entity.Name && t.ID != entity.ID);
            if (exists)
                throw new DomainException(String.Format("名称 {0} 已使用，不能重复添加。", entity.Name));
            return true;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 套餐子项服务
    /// </summary>
    public class ComboItemService : DomainServiceBase<IRepository<ComboItem>, ComboItem, Guid>, IComboItemService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComboItemService()
        {
        }

        protected override bool DoValidate(ComboItem entity)
        {
            if (entity.Quantity < 0)
                throw new DomainException("数量需大于零");
            return true;
        }

        public override ComboItem Add(ComboItem entity)
        {
            if (entity == null)
                return null;
            var productCodeList = this.Repository.GetQueryable(false).Where(t => t.ProductID == entity.ProductID).Select(t => t.ProductCode).ToList();
            var existData = false;
            foreach (var productCode in productCodeList)
            {
                if (productCode == entity.ProductCode)
                    existData = true;
            }
            if (existData)
                throw new DomainException("数据已存在");
            entity.ID = Util.NewID();
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="comboItems"></param>
        /// <returns></returns>
        public bool BatchAdd(IEnumerable<ComboItem> comboItems)
        {
            if (comboItems == null || comboItems.Count() < 1)
                throw new DomainException("没有数据");
            var comboItemList = comboItems.ToList();
            comboItemList.ForEach(t =>
            {
                t.ID = Util.NewID();
                t.CreatedDate = DateTime.Now;
                t.MerchantID = AppContext.CurrentSession.MerchantID;
            });
            return this.Repository.AddRange(comboItemList).Count() > 0;
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
            var comboItems = this.Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
            if (comboItems == null || comboItems.Count() < 1)
                throw new DomainException("数据不存在");
            return this.Repository.DeleteRange(comboItems) > 0;
        }

        /// <summary>
        /// 查询推送服务子项
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<ComboItem> Search(ComboItemFilter filter, out int totalCount)
        {
            var queryable = this.Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.ProductID.HasValue)
                queryable = queryable.Where(t => t.ProductID == filter.ProductID.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }
    }
}

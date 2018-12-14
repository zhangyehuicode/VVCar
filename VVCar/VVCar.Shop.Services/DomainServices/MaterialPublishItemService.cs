using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Services;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Enums;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 信息推送子项服务
    /// </summary>
    public class MaterialPublishItemService : DomainServiceBase<IRepository<MaterialPublishItem>, MaterialPublishItem, Guid>, IMaterialPublishItemService
    {
        public MaterialPublishItemService()
        {
        }

        IRepository<MaterialPublish> MaterialPublishRepo { get => UnitOfWork.GetRepository<IRepository<MaterialPublish>>(); }

        
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="materialPublishItems"></param>
        /// <returns></returns>
        public bool BatchAdd(IEnumerable<MaterialPublishItem> materialPublishItems)
        {
            if (materialPublishItems == null || materialPublishItems.Count() < 1)
                throw new DomainException("没有数据");
            var materialPublishItemList = materialPublishItems.ToList();
            var materialPublishID = materialPublishItemList.FirstOrDefault().MaterialPublishID;
            var materialPublish = MaterialPublishRepo.GetByKey(materialPublishID);
            if (materialPublish.Status == EMaterialPublishStatus.Published)
                throw new DomainException("已发布的数据不允许新增素材!");
            var materialIDs = materialPublishItemList.Select(t => t.MaterialID).Distinct();
            var existData = Repository.GetQueryable(false)
                .Where(t => t.MaterialPublishID == materialPublishID && materialIDs.Contains(t.MaterialID))
                .Select(t => t.MaterialID).ToList();
            if (existData.Count > 0)
            {
                materialPublishItemList.RemoveAll(t => existData.Contains(t.MaterialID));
            }
            if (materialPublishItemList.Count < 1)
                return true;
            var count = 0;
            materialPublishItemList.ForEach(t =>
            {
                t.ID = Util.NewID();
                t.Index = GenerateIndex(t.MaterialPublishID) + count;
                t.MerchantID = AppContext.CurrentSession.MerchantID;
                t.CreatedUserID = AppContext.CurrentSession.UserID;
                t.CreatedUser = AppContext.CurrentSession.UserName;
                t.CreatedDate = DateTime.Now;
                count++;
            });
            return Repository.AddRange(materialPublishItemList).Count() > 0;
        }

        private int GenerateIndex(Guid id)
        {
            var index = 1;
            var indexList = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.ID == id).Select(t => t.Index);
            if (indexList.Count() > 0)
                index = indexList.Max() + 1;
            return index;
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
            var materialItemList = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
            if (materialItemList == null || materialItemList.Count < 1)
                throw new DomainException("数据不存在");
            var materialPublishIDs = materialItemList.Select(t => t.MaterialPublishID).Distinct();
            var materialPublishList = MaterialPublishRepo.GetQueryable(false).Where(t => materialPublishIDs.Contains(t.ID)).ToList();
            materialPublishList.ForEach(t =>
            {
                if (t.Status == EMaterialPublishStatus.Published)
                    throw new DomainException("已发布的数据不允许删除!");
            });
            return Repository.DeleteRange(materialItemList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<MaterialPublishItemDto> Search(MaterialPublishItemFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.MaterialPublishID.HasValue)
                queryable = queryable.Where(t => t.MaterialPublishID == filter.MaterialPublishID.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderBy(t => t.Index).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.OrderBy(t => t.Index).MapTo<MaterialPublishItemDto>().ToArray();
        }

        /// <summary>
        /// 调整索引
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public bool AdjustIndex(AdjustIndexParam param)
        {
            if (param == null || param.ID == null)
                return false;
            var pointMaterialPublishItem = Repository.GetByKey(param.ID);
            if (pointMaterialPublishItem == null)
                return false;
            var materialPublishItemQueryable = Repository.GetQueryable().Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.MaterialPublishID == pointMaterialPublishItem.MaterialPublishID);
            MaterialPublishItem exchangeEntity = null;
            if (param.Direction == EAdjustDirection.Up)
            {
                var pre = materialPublishItemQueryable.Where(t => t.Index == (pointMaterialPublishItem.Index - 1)).FirstOrDefault();
                if (pre == null)
                    return true;
                else
                    exchangeEntity = pre;
            }
            else if (param.Direction == EAdjustDirection.Down)
            {
                var next = materialPublishItemQueryable.Where(t => t.Index == (pointMaterialPublishItem.Index + 1)).FirstOrDefault();
                if (next == null)
                    return true;
                else
                    exchangeEntity = next;
            }
            if (exchangeEntity != null)
            {
                var exchangeIndex = exchangeEntity.Index;
                exchangeEntity.Index = pointMaterialPublishItem.Index;
                pointMaterialPublishItem.Index = exchangeIndex;
                return Repository.Update(new List<MaterialPublishItem> { exchangeEntity, pointMaterialPublishItem }) == 2;
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Enums;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    public partial class MaterialPublishService : DomainServiceBase<IRepository<MaterialPublish>, MaterialPublish, Guid>, IMaterialPublishService
    {
        public MaterialPublishService()
        {
        }

        #region properties

        IRepository<MaterialPublishItem> MaterialPublishItemRepo { get => UnitOfWork.GetRepository<IRepository<MaterialPublishItem>>(); }

        IRepository<MakeCodeRule> MakeCodeRuleRepo { get => UnitOfWork.GetRepository<IRepository<MakeCodeRule>>(); }
        
        #endregion 

        public override MaterialPublish Add(MaterialPublish entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            if (string.IsNullOrEmpty(entity.Code))
                entity.Code = GetTradeNo();
            var existNo = Repository.Exists(t => t.Code == entity.Code);
            if (existNo)
                throw new DomainException($"创建信息发布单号失败，单号{entity.Code}已存在");
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        private string GetTradeNo()
        {
            var newTradeNo = string.Empty;
            var existNo = false;
            var makeCodeRuleService = ServiceLocator.Instance.GetService<IMakeCodeRuleService>();
            var entity = Repository.GetQueryable(false).OrderByDescending(t => t.CreatedDate).FirstOrDefault();
            if (entity != null && entity.CreatedDate.Date != DateTime.Now.Date)
            {
                var rule = MakeCodeRuleRepo.GetQueryable().Where(t => t.Code == "MaterialPublish" && t.IsAvailable).FirstOrDefault();
                if (rule != null)
                {
                    rule.CurrentValue = 0;
                    MakeCodeRuleRepo.Update(rule);
                }
            }
            do
            {
                newTradeNo = makeCodeRuleService.GenerateCode("MaterialPublish", DateTime.Now);
                existNo = Repository.Exists(t => t.Code == newTradeNo);
            } while (existNo);
            return newTradeNo;
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
            var materialPushList = Repository.GetInclude(t => t.MaterialPublishItems).Where(t => ids.Contains(t.ID) && t.Status == EMaterialPublishStatus.NotPublish).ToList();
            if (materialPushList == null || materialPushList.Count() < 1)
                throw new DomainException("请选择未发布的信息");
            UnitOfWork.BeginTransaction();
            try
            {
                foreach (var materialPush in materialPushList)
                {
                    if (materialPush.MaterialPublishItems.Count() > 0)
                    {
                        MaterialPublishItemRepo.DeleteRange(materialPush.MaterialPublishItems);
                        materialPush.MaterialPublishItems = null;
                    }
                }
                this.Repository.Delete(materialPushList);
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw e;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(MaterialPublish entity)
        {
            if (entity == null)
                return false;
            var materialPublish = Repository.GetByKey(entity.ID);
            if (materialPublish == null)
                return false;
            if (materialPublish.Status == EMaterialPublishStatus.Published)
                throw new DomainException("已经发布的不能修改");
            materialPublish.Name = entity.Name;
            materialPublish.LastUpdateUserID = AppContext.CurrentSession.UserID;
            materialPublish.LastUpdateUser = AppContext.CurrentSession.UserName;
            materialPublish.LastUpdateDate = DateTime.Now;
            return Repository.Update(materialPublish) > 0;
        }

        /// <summary>
        /// 批量发布
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool BatchHandMaterialPublish(Guid[] ids)
        {
            UnitOfWork.BeginTransaction();
            try
            {
                if (ids == null || ids.Length < 1)
                    throw new DomainException("参数不正确");
                if (ids.Length > 1)
                    throw new DomainException("一次只能发布一条广告!");
                var notPublishData = this.Repository.GetInclude(t => t.MaterialPublishItems, false).Where(t => ids.Contains(t.ID) && (EMaterialPublishStatus.NotPublish == t.Status || EMaterialPublishStatus.CancelPublish == t.Status)).ToList();
                if (notPublishData.Count < 1)
                    throw new DomainException("请选择未发布或取消发布的广告");
                var notExistItem = false;
                notPublishData.ForEach(t =>
                {
                    if (t.MaterialPublishItems.Count() < 1)
                        notExistItem = true;
                });
                if (notExistItem)
                    throw new DomainException("请先添加素材");
                var updaterecordList = Repository.GetQueryable(true).Where(t=> t.MerchantID == AppContext.CurrentSession.MerchantID && t.Status == EMaterialPublishStatus.Publishing).ToList();
                updaterecordList.ForEach(t =>
                {
                    t.Status = EMaterialPublishStatus.Published;
                });
                Repository.UpdateRange(updaterecordList);
                var materialList = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
                materialList.ForEach(t =>
                {
                    t.Status = EMaterialPublishStatus.Publishing;
                    t.PublishDate = DateTime.Now;
                    t.LastUpdateDate = DateTime.Now;
                    t.LastUpdateUser = AppContext.CurrentSession.UserName;
                    t.LastUpdateUserID = AppContext.CurrentSession.UserID;
                });
                Repository.UpdateRange(materialList);
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException("发布失败" + e.Message);
            }         
        }

        /// <summary>
        /// 批量取消发布
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool BatchHandCancelMaterialPublish(Guid[] ids)
        {
            UnitOfWork.BeginTransaction();
            try
            {
                if (ids == null || ids.Length < 1)
                    throw new DomainException("参数不正确");
                var publishedData = this.Repository.GetInclude(t => t.MaterialPublishItems, false).Where(t => ids.Contains(t.ID) && (t.Status == EMaterialPublishStatus.Published || t.Status == EMaterialPublishStatus.Publishing)).ToList();
                if (publishedData.Count < 1)
                    throw new DomainException("请选择已发布或者发布中的数据");
                var materialList = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
                materialList.ForEach(t =>
                {
                    t.Status = EMaterialPublishStatus.CancelPublish;
                    t.LastUpdateDate = DateTime.Now;
                    t.LastUpdateUser = AppContext.CurrentSession.UserName;
                    t.LastUpdateUserID = AppContext.CurrentSession.UserID;
                });
                Repository.UpdateRange(materialList);
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException("取消发布失败" + e.Message);
            }
            
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<MaterialPublish> Search(MaterialPublishFilter filter, out int totalCount)
        {
            var queryable = this.Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.Name))
                queryable = queryable.Where(t => t.Name.Contains(filter.Name));
            if (filter.Status.HasValue)
                queryable = queryable.Where(t => t.Status == filter.Status);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }
    }
}

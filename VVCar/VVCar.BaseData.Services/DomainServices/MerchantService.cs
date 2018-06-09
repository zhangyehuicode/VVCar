using System;
using System.Collections.Generic;
using System.Linq;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using YEF.Core.Dtos;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Dtos;

namespace VVCar.BaseData.Services.DomainServices
{
    /// <summary>
    /// 商户领域服务
    /// </summary>
    public class MerchantService : DomainServiceBase<IRepository<Merchant>, Merchant, Guid>, IMerchantService
    {
        public MerchantService()
        {
        }

        protected override bool DoValidate(Merchant entity)
        {
            if (Repository.Exists(t => (t.Code == entity.Code || t.Name == entity.Name) && t.ID != entity.ID))
            {
                throw new DomainException("商户号或商户名称重复");
            }
            var merchant = Repository.GetByKey(entity.ID, false);
            if (merchant != null)
            {
                if (merchant.Code != entity.Code)
                    throw new DomainException("商户号不允许修改");
            }
            return true;
        }

        /// <summary>
        /// 生成9位随机商户号
        /// </summary>
        /// <returns></returns>
        public string GenerateCode()
        {
            var code = string.Empty;
            for (var i = 0; i < 9; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());
                code += r.Next(0, 10);
            }
            return code;
        }

        /// <summary>
        /// 生成商户号
        /// </summary>
        /// <returns></returns>
        public string GenerateMerchantCode()
        {
            var count = 0;
            var code = GenerateCode();
            var exists = Repository.Exists(t => t.Code == code);
            while (exists && count < 20)
            {
                code = GenerateCode();
                exists = Repository.Exists(t => t.Code == code);
                count++;
            }
            return code;
        }

        public override Merchant Add(Merchant entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.IsHQ = false;
            entity.Code = GenerateMerchantCode();
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.MerchantID = Guid.Parse("00000000-0000-0000-0000-000000000001");
            return base.Add(entity);
        }

        public override bool Delete(Guid key)
        {
            var entity = Repository.GetByKey(key);
            if (entity == null)
                return false;
            entity.IsDeleted = true;
            entity.LastUpdatedDate = DateTime.Now;
            entity.LastUpdatedUserID = AppContext.CurrentSession.UserID;
            entity.LastUpdatedUser = AppContext.CurrentSession.UserName;
            return Repository.Update(entity) > 0;
        }

        public override bool Update(Merchant entity)
        {
            if (entity == null)
                return false;
            var merchant = Repository.GetByKey(entity.ID);
            if (merchant == null)
                return false;
            merchant.Name = entity.Name;
            merchant.LegalPerson = entity.LegalPerson;
            merchant.MobilePhoneNo = entity.MobilePhoneNo;
            merchant.BusinessLicenseImgUrl = entity.BusinessLicenseImgUrl;
            merchant.CompanyAddress = entity.CompanyAddress;
            merchant.WeChatAppID = entity.WeChatAppID;
            merchant.WeChatAppSecret = entity.WeChatAppSecret;
            merchant.WeChatMchID = entity.WeChatMchID;
            merchant.WeChatMchKey = entity.WeChatMchKey;
            merchant.LastUpdatedDate = DateTime.Now;
            merchant.LastUpdatedUserID = AppContext.CurrentSession.UserID;
            merchant.LastUpdatedUser = AppContext.CurrentSession.UserName;
            return Repository.Update(merchant) > 0;
        }

        public IEnumerable<Merchant> Search(MerchantFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false);
            if (filter.ID.HasValue)
                queryable = queryable.Where(t => t.ID == filter.ID.Value);
            if (!string.IsNullOrEmpty(filter.Code))
                queryable = queryable.Where(t => t.Code.Contains(filter.Code));
            if (!string.IsNullOrEmpty(filter.Name))
                queryable = queryable.Where(t => t.Name.Contains(filter.Name));
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderBy(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }
    }
}

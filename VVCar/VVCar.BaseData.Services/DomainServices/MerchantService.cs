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
            if (Repository.Exists(t => t.Code == entity.Code || t.Name == entity.Name))
            {
                throw new DomainException("商户号或商户名称重复");
            }
            return base.DoValidate(entity);
        }

        public override Merchant Add(Merchant entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.IsHQ = false;
            entity.Code = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }

        public IEnumerable<Merchant> Search(MerchantFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false);
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

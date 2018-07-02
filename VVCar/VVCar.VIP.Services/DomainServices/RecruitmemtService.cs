using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 人才招聘领域服务
    /// </summary>
    public class RecruitmemtService : DomainServiceBase<IRepository<Recruitment>, Recruitment, Guid>, IRecruitmentService
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="recruitmentService"></param>
        public RecruitmemtService(IRecruitmentService recruitmentService)
        {
            RecruitmentService = recruitmentService;
        }

        IRecruitmentService RecruitmentService { get; set; }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<Recruitment> Search(RecruitmentFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false);
            if (string.IsNullOrEmpty(filter.Recruiter))
                queryable = queryable.Where(t => t.Recruiter.Contains(filter.Recruiter));
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderBy(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }
    }
}

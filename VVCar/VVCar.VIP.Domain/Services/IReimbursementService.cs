using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 业务报销服务接口
    /// </summary>
    public interface IReimbursementService : IDomainService<IRepository<Reimbursement>, Reimbursement, Guid>
    {
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchDelete(Guid[] ids);

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool ApproveReimbursement(Guid[] ids);

        /// <summary>
        /// 批量反审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool AntiApproveReimbursement(Guid[] ids);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<ReimbursementDto> Search(ReimbursementFilter filter, out int totalCount);
    }
}

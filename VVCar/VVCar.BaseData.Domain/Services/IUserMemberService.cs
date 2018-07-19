using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;
using YEF.Core.Dtos;

namespace VVCar.BaseData.Domain.Services
{
    /// <summary>
    /// 用户会员关联服务接口
    /// </summary>
    public partial interface IUserMemberService : IDomainService<IRepository<UserMember>, UserMember, Guid>
    {
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="userMembers"></param>
        /// <returns></returns>
        bool BatchAdd(IEnumerable<UserMember> userMembers);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchDelete(IEnumerable<Guid> ids);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<UserMemberDto> Search(UserMemberFilter filter, out int totalCount);
    }
}

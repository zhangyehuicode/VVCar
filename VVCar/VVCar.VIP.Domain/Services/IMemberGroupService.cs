using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 会员分组 领域服务接口
    /// </summary>
    public interface IMemberGroupService : IDomainService<IRepository<MemberGroup>, MemberGroup, Guid>
    {
        /// <summary>
        /// 获取默认会员分组ID
        /// </summary>
        /// <returns></returns>
        Guid GetDefaultMemberGroupID();

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<MemberGroup> Search(MemberGroupFilter filter, out int totalCount);

        /// <summary>
        /// 获取精简结构数据
        /// </summary>
        /// <returns></returns>
        IList<IDCodeNameDto> GetLiteData();

        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        IEnumerable<MemberGroupTreeDto> GetTreeData(Guid? parentID);

        /// <summary>
        /// 获取树形数据包含会员信息
        /// </summary>
        /// <param name="cardNumberOrName"></param>
        /// <returns></returns>
        IEnumerable<MemberGroupTreeDto> GetTreeDataContainsMember(MemberGroupFilter filter);
    }
}

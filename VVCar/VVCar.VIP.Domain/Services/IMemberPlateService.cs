using System;
using System.Collections.Generic;
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
    public interface IMemberPlateService : IDomainService<IRepository<MemberPlate>, MemberPlate, Guid>
    {
        /// <summary>
        /// 通过MemberID获取会员信息
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        MemberDto GetMemberByMemberID(Guid memberID);

        /// <summary>
        /// 通过车牌获取会员
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<MemberDto> GetMemberByPlate(MemberPlateFilter filter, ref int totalCount);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<MemberPlate> Search(MemberPlateFilter filter, ref int totalCount);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Filters;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 会员等级 领域服务接口
    /// </summary>
    public interface IMemberGradeService : IDomainService<IRepository<MemberGrade>, MemberGrade, Guid>
    {
        /// <summary>
        /// 获取默认会员等级
        /// </summary>
        /// <returns></returns>
        MemberGrade GetDefaultMemberGrade();

        /// <summary>
        /// 会员等级查询
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns></returns>
        IEnumerable<MemberGrade> Search(MemberGradeFilter filter, out int totalCount);

        /// <summary>
        /// 设置等级状态
        /// </summary>
        /// <param name="memberGradeID">The grade identifier.</param>
        /// <param name="status">status</param>
        /// <returns></returns>
        bool ChangeStatus(Guid memberGradeID, EMemberGradeStatus status);

        /// <summary>
        /// 使用会员权益
        /// </summary>
        /// <param name="memberID">会员ID</param>
        /// <param name="isCousome">是否是消费操作</param>
        /// <param name="tradeAmount">交易金额</param>
        UseMemberGradeRightsResult UseMemberGradeRights(Guid memberID, bool isCousome, decimal tradeAmount);

        /// <summary>
        /// 检查会员降级
        /// </summary>
        void CheckGradeDegrade();

        /// <summary>
        /// 获取支持购买的会员等级
        /// </summary>
        /// <returns></returns>
        IEnumerable<MemberGradeIntroDto> GetCanPurchaseGradeList(Guid? currentGradeID);

        ///// <summary>
        ///// 获取所有会员产品权益
        ///// </summary>
        ///// <returns></returns>
        //IEnumerable<MemberRightDto> GetAllGradeProductRights();

        /// <summary>
        /// 获取会员折扣权益
        /// </summary>
        /// <param name="openid">The openid.</param>
        /// <returns></returns>
        IEnumerable<IDCodeNameDto> GetGradeDiscountRights(string openid);

        /// <summary>
        /// 设置开闭状态
        /// </summary>
        /// <param name="memberGradeID">The member grade identifier.</param>
        /// <param name="isNotOpen">if set to <c>true</c> [is not open].</param>
        /// <returns></returns>
        bool ChangeOpen(Guid memberGradeID, bool isNotOpen);
    }
}

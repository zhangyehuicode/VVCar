using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Domain.Services
{
    public interface IPickUpOrderService : IDomainService<IRepository<PickUpOrder>, PickUpOrder, Guid>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<PickUpOrder> Search(PickUpOrderFilter filter, ref int totalCount);

        /// <summary>
        /// 结账
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        bool CheckOut(string code);

        /// <summary>
        /// 获取接车单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PickUpOrder GetOrder(Guid id);

        /// <summary>
        /// 重新计算订单金额并保存
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        bool RecountMoneySave(string code);

        /// <summary>
        /// 核销
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool Verification(VerificationParam param);

        /// <summary>
        /// 获取会员接车单
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        IEnumerable<PickUpOrderDto> GetMemberPickUpOrder(Guid memberId);
    }
}

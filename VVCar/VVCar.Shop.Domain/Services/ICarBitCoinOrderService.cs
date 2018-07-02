using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Domain.Services
{
    /// <summary>
    /// 车比特订单领域服务接口
    /// </summary>
    public interface ICarBitCoinOrderService : IDomainService<IRepository<CarBitCoinOrder>, CarBitCoinOrder, Guid>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns></returns>
        IEnumerable<CarBitCoinOrder> Search(OrderFilter filter, out int totalCount);

        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <returns></returns>
        string GetTradeNo();

        /// <summary>
        /// 重新计算订单金额
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNotify"></param>
        void RecountMoney(CarBitCoinOrder entity, bool isNotify = false);

        /// <summary>
        /// 重新计算订单金额并保存
        /// </summary>
        /// <param name="code"></param>
        /// <param name="isNotify"></param>
        /// <returns></returns>
        bool RecountMoneySave(string code, bool isNotify = false);
    }
}

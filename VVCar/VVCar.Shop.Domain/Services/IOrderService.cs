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
    /// <summary>
    /// 订单领域服务接口
    /// </summary>
    public interface IOrderService : IDomainService<IRepository<Order>, Order, Guid>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns></returns>
        IEnumerable<OrderDto> Search(OrderFilter filter, out int totalCount);

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
        void RecountMoney(Order entity, bool isNotify = false);

        /// <summary>
        /// 重新计算订单金额并保存
        /// </summary>
        /// <param name="code"></param>
        /// <param name="isNotify"></param>
        /// <returns></returns>
        bool RecountMoneySave(string code, bool isNotify = false);

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        bool Delivery(Order order);

        /// <summary>
        /// 取消发货
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool AntiDelivery(Guid id);

        /// <summary>
        /// 手动发送回执
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool RevisitTips(Guid id);

        /// <summary>
        /// 发送回执任务
        /// </summary>
        /// <returns></returns>
        bool RevisitTipsTask();
    }
}

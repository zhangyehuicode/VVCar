﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 消费历史Dto
    /// </summary>
    public class ConsumeHistoryDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 交易单号
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 消费项目
        /// </summary>
        public string Consumption { get; set; }

        /// <summary>
        /// 消费数量
        /// </summary>
        public decimal TradeCount{ get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        public decimal TradeMoney { get; set; }

        /// <summary>
        /// 商品成本
        /// </summary>
        public decimal BasePrice { get; set; }

        /// <summary>
        /// 毛利
        /// </summary>
        public decimal GrossProfit { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public EHistorySource? Source { get; set; }

        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime? CreatedDate { get; set; }
    }

    /// <summary>
    /// 消费历史数据来源
    /// </summary>
    public enum EHistorySource
    {
        /// <summary>
        /// 接车单
        /// </summary>
        PickUpOrder = 0,

        /// <summary>
        /// 商城订单
        /// </summary>
        ShopOrder = 1,

        /// <summary>
        /// 历史导入数据
        /// </summary>
        HistoryData = 2,
    }
}

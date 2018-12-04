using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Dtos
{
    public class DailyExpenseDto
    {
        /// <summary>
        /// 支出日期
        /// </summary>
        public DateTime? ExpenseDate { get; set; }

        /// <summary>
        /// 支出金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 工作人员数量
        /// </summary>
        public int? StaffCount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedDate { get; set; }
    }
}

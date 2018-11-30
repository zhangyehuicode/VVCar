using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    public class DailyExpenseFilter : BasePageFilter
    {
        /// <summary>
        /// 支出日期
        /// </summary>
        public DateTime? ExpenseDate { get; set; }
    }
}

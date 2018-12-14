using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    public class MaterialFilter : BasePageFilter
    {
        /// <summary>
        /// 素材名称
        /// </summary>
        public string Name { get; set; }
    }
}

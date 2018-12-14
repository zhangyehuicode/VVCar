using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    public class MaterialPublishItemFilter : BasePageFilter
    {
        public Guid? MaterialPublishID { get; set; }

        public Guid MaterialID { get; set; }
    }
}

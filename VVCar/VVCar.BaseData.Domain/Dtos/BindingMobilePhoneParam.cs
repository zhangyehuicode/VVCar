using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.BaseData.Domain.Dtos
{
    /// <summary>
    /// 手机绑定参数
    /// </summary>
    public class BindingMobilePhoneParam
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhoneNo { get; set; }
    }
}

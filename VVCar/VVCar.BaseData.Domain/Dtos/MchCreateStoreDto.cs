using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VVCar.BaseData.Domain.Dtos
{
    /// <summary>
    /// 商户开店DTO
    /// </summary>
    public class MchCreateStoreDto
    {
        /// <summary>
        /// 门店编号
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNo { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 门店管理员姓名
        /// </summary>
        [Required]
        public string StoreAdmin { get; set; }

        /// <summary>
        /// 门店管理员手机号码
        /// </summary>
        [Required]
        public string StoreAdminPhoneNumber { get; set; }

        /// <summary>
        /// 门店管理员密码
        /// </summary>
        public string StoreAdminPassword { get; set; }
    }
}

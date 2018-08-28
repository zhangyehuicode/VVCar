using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace VVCar.BaseData.Domain.Dtos
{
    /// <summary>
    /// 门店位置信息参数
    /// </summary>
    public class DepartmentLocationDto
    {
        /// <summary>
        ///经度
        /// </summary>
        [Display(Name = "经度")]
        public double Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [Display(Name = "纬度")]
        public double Latitude { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
        public string Address { get; set; }

        /// <summary>
        /// 位置名
        /// </summary>
        [Display(Name = "位置名")]
        public string LocationName { get; set; }

        /// <summary>
        /// 在查看位置界面底部显示的超链接,可点击跳转
        /// </summary>
        [Display(Name = "在查看位置界面底部显示的超链接,可点击跳转")]
        public string InfoUrl { get; set; }

        /// <summary>
        /// 修改人ID
        /// </summary>
        [Display(Name = "修改人ID")]
        public Guid? UpdateUserID { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [Display(Name = "修改人")]
        public string UpdateUser { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using YEF.Core.Data;
using YEF.Core.Enums;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 门店
    /// </summary>
    public class Department : EntityBase
    {
        /// <summary>
        /// 门店编号
        /// </summary>
        [Display(Name = "门店编号")]
        public string Code { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        [Display(Name = "门店名称")]
        public string Name { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Display(Name = "联系人")]
        public string ContactPerson { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Display(Name = "联系电话")]
        public string ContactPhoneNo { get; set; }

        /// <summary>
        /// 联系手机
        /// </summary>
        [Display(Name = "联系手机")]
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        [Display(Name = "电子邮件")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
        public string Address { get; set; }

        /// <summary>
        /// 地区分区ID
        /// </summary>
        [Display(Name = "地区分区")]
        public string DistrictRegion { get; set; }

        /// <summary>
        /// 管理分区ID
        /// </summary>
        [Display(Name = "管理分区")]
        public string AdministrationRegion { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid CreatedUserID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [Display(Name = "最后修改人ID")]
        public Guid? LastUpdateUserID { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [Display(Name = "最后修改人")]
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Display(Name = "最后修改时间")]
        public DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        [Display(Name = "数据来源")]
        public EDataSource DataSource { get; set; }

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
        /// 位置名
        /// </summary>
        [Display(Name = "位置名")]
        public string LocationName { get; set; }

        /// <summary>
        /// 在查看位置界面底部显示的超链接,可点击跳转
        /// </summary>
        [Display(Name = "在查看位置界面底部显示的超链接,可点击跳转")]
        public string InfoUrl { get; set; }
    }
}

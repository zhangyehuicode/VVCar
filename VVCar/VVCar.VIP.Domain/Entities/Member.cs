using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Enums;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;
using VVCar.VIP.Domain.Enums;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 会员
    /// </summary>
    public class Member : EntityBase
    {
        public Member()
        {
            MemberPlateList = new List<MemberPlate>();
        }

        /// <summary>
        /// 会员卡ID
        /// </summary>
        [Display(Name = "会员卡ID")]
        public Guid CardID { get; set; }

        /// <summary>
        /// 会员卡
        /// </summary>
        [Display(Name = "会员卡")]
        public virtual MemberCard Card { get; set; }

        /// <summary>
        /// 会员卡号
        /// </summary>
        [Display(Name = "会员卡号")]
        public string CardNumber { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public ESex Sex { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Display(Name = "手机号码")]
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Display(Name = "生日")]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 微信OpenID
        /// </summary>
        [Display(Name = "微信OpenID")]
        public string WeChatOpenID { get; set; }

        /// <summary>
        /// 会员积分
        /// </summary>
        [Display(Name = "会员积分")]
        public double Point { get; set; }

        /// <summary>
        /// 手机号码归属地
        /// </summary>
        [Display(Name = "手机号码归属地")]
        public string PhoneLocation { get; set; }

        /// <summary>
        /// 归属门店ID(第一次消费所在门店)
        /// </summary>
        [Display(Name = "归属门店ID")]
        public Guid? OwnerDepartmentID { get; set; }

        /// <summary>
        /// 归属门店(第一次消费所在门店)
        /// </summary>
        public virtual Department OwnerDepartment { get; set; }

        /// <summary>
        /// 会员来源
        /// </summary>
        [Display(Name = "会员来源")]
        public EMemberSource Source { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public Guid? CreatedUserID { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [Display(Name = "创建人")]
        public String CreatedUser { get; set; }

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
        /// 最后修改人姓名
        /// </summary>
        [Display(Name = "最后修改人")]
        public String LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Display(Name = "最后修改时间")]
        public DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// 会员分组ID
        /// </summary>
        [Display(Name = "会员分组ID")]
        public Guid? MemberGroupID { get; set; }

        /// <summary>
        /// 会员分组
        /// </summary>
        public virtual MemberGroup MemberGroup { get; set; }

        /// <summary>
        /// 会员等级ID
        /// </summary>
        [Display(Name = "会员等级ID")]
        public Guid? MemberGradeID { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        public virtual MemberGrade MemberGrade { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public virtual ICollection<MemberPlate> MemberPlateList { get; set; }

        /// <summary>
        /// 保险到期时间
        /// </summary>
        [Display(Name = "保险到期时间")]
        public DateTime? InsuranceExpirationDate { get; set; }

        ///// <summary>
        ///// 会员签到
        ///// </summary>
        //[Display(Name = "会员签到")]
        //public virtual ICollection<MemberSignIn> MemberSignIn { get; set; }
    }
}

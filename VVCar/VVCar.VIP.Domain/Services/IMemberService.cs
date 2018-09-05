using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Domain;
using YEF.Core.Data;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Dtos;
using YEF.Core.Dtos;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Enums;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 会员领域服务接口
    /// </summary>
    public partial interface IMemberService : IDomainService<IRepository<Member>, Member, Guid>
    {
        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="memberLoginDto"></param>
        /// <returns></returns>
        MemberCardDto MemberLogin(MemberLoginDto memberLoginDto);

        /// <summary>
        /// 查询会员信息
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        PagedResultDto<MemberDto> Search(MemberFilter filter);

        ///// <summary>
        ///// 获取会员分析-用户明细分页数据
        ///// </summary>
        ///// <param name="filter">分页参数</param>
        ///// <returns></returns>
        //PagedResultDto<MemberDetailDto> GetMemberDetail(MemberDetailFilter filter);

        ///// <summary>
        ///// 获取会员分析-新增用户
        ///// </summary>
        ///// <param name="filter"></param>
        ///// <returns></returns>
        //IEnumerable<NewMemberCountOfDateRange> GetNewMemberCount(NewMemberFilter filter);

        /// <summary>
        /// 根据会员卡号获取会员信息
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        Member GetMemberByCardNumber(string cardNumber);

        /// <summary>
        /// 根据手机号码获取会员信息
        /// </summary>
        /// <param name="mobilePhoneNo"></param>
        /// <returns></returns>
        Member GetMemberByMobilePhone(string mobilePhoneNo);

        /// <summary>
        /// 根据微信OpenID获取会员信息
        /// </summary>
        /// <param name="openID"></param>
        /// <returns></returns>
        MemberCardDto GetMemberInfoByWeChat(string openID, bool isagentdept = false);

        /// <summary>
        /// 获取顾客会员信息
        /// </summary>
        /// <param name="mobilePhoneNo">手机号码</param>
        /// <param name="wechatOpenID">微信openID</param>
        /// <returns></returns>
        MemberDto GetCustomerMemberInfo(string mobilePhoneNo, string wechatOpenID);

        /// <summary>
        /// 获取会员Lite信息
        /// </summary>
        /// <returns></returns>
        MemberLiteInfoDto GetMemberLiteInfo(EMemberFindType findType, string keyword);

        ///// <summary>
        ///// 获取会员Nano信息
        ///// </summary>
        ///// <returns></returns>
        //MemberNanoInfoDto GetMemberNanoInfo(EMemberFindType findType, string keyword);

        ///// <summary>
        ///// 根据账号密码获取会员Nano信息
        ///// </summary>
        ///// <returns></returns>
        //MemberNanoInfoDto GetMemberNanoInfoByPassword(string account, string password);

        /// <summary>
        /// 获取会员基本信息
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        MemberBaseInfoDto GetBaseInfo(Guid memberId);

        /// <summary>
        /// 挂失
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        bool ReportLoss(string cardNumber);

        /// <summary>
        /// 解挂
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        bool CancelLoss(string cardNumber);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="changePwdDto"></param>
        /// <returns></returns>
        bool ChangePassword(ChangePasswordDto changePasswordDto);

        /// <summary>
        /// 会员注册，微信渠道
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        MemberCardDto Register(MemberRegisterDto registerDto);

        /// <summary>
        /// 重置密码，将密码重置为123456
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        bool ResetPassword(Guid memberID);

        ///// <summary>
        ///// 换卡
        ///// </summary>
        ///// <param name="changeCardDto"></param>
        ///// <returns></returns>
        //bool ChangeCard(ChangeCardDto changeCardDto);

        /// <summary>
        /// 获取号码归属地
        /// </summary>
        /// <param name="phoneNumber">手机号码</param>
        /// <returns></returns>
        string GetPhoneLoaction(string phoneNumber);

        ///// <summary>
        ///// 会员统计信息
        ///// </summary>
        ///// <returns></returns>
        //StatisticDto Statistic();

        /// <summary>
        /// 更新会员信息
        /// </summary>
        /// <param name="updateMember"></param>
        /// <returns></returns>
        bool UpdateMemberInfoForWeChat(UpdateMemberDto updateMember);

        ///// <summary>
        ///// 会员分析->人数分析->获取用户明细
        ///// </summary>
        ///// <param name="filter"></param>
        ///// <returns></returns>
        //IEnumerable<MemberDetailDto> GetMemberDetailForConsumeAnalysis(MemberDetailFilter filter, ref int totalCount);

        /// <summary>
        /// 移动会员分组
        /// </summary>
        /// <param name="changeDto"></param>
        /// <returns></returns>
        bool ChangeMemberGroup(ChangeMemberGroupDto changeDto);

        /// <summary>
        /// 调整会员积分
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="pointType"></param>
        /// <param name="adjustPoints"></param>
        /// <returns></returns>
        bool AdjustMemberPoint(string openId, EMemberPointType pointType, double adjustPoints = 0);

        /// <summary>
        /// 调整会员积分
        /// </summary>
        /// <param name="memberID">会员ID</param>
        /// <param name="pointType">调整积分类型</param>
        /// <param name="adjustPoints">调整积分值</param>
        /// <param name="outTradeNo">外部交易流水号</param>
        /// <returns></returns>
        bool AdjustMemberPoint(Guid memberID, EMemberPointType pointType, double adjustPoints, string outTradeNo = "");

        /// <summary>
        /// 设置会员等级
        /// </summary>
        /// <param name="memberID">会员ID</param>
        /// <param name="memberGradeID">会员等级ID</param>
        /// <returns></returns>
        bool SetMemberGrade(Guid memberID, Guid memberGradeID);

        /// <summary>
        /// 设置会员等级
        /// </summary>
        /// <param name="memberID">会员ID</param>
        /// <param name="memberGrade">会员等级</param>
        /// <returns></returns>
        bool SetMemberGrade(Guid memberID, MemberGrade memberGrade);

        ///// <summary>
        ///// 校验会员是否享有POS权益
        ///// </summary>
        ///// <param name="memberCardNumber">会员卡号</param>
        ///// <param name="posRightID">POS权益ID</param>
        //bool CheckMemberPosRight(string memberCardNumber, Guid posRightID);

        ///// <summary>
        ///// 获取会员连续签到记录
        ///// </summary>
        ///// <param name="filter"></param>
        ///// <returns></returns>
        //IEnumerable<MemberSignInDto> GetMemberSignInOfContinuous(MemberSignInFilter filter);

        ///// <summary>
        ///// 签到
        ///// </summary>
        ///// <param name="openId"></param>
        ///// <returns></returns>
        //int SignIn(string openId);

        ///// <summary>
        ///// 预览积分抵扣pos金额
        ///// </summary>
        ///// <param name="cardNumber">The card number.</param>
        ///// <param name="paymentAmount">The order amount.</param>
        ///// <returns></returns>
        //PreviewUsePointPaymentResultDto PreviewUsePointPayment(string cardNumber, decimal paymentAmount);

        ///// <summary>
        ///// 获取会员可购买的会员等级
        ///// </summary>
        ///// <param name="memberID">会员ID</param>
        ///// <returns></returns>
        //IEnumerable<MemberGradeIntroDto> GetMemberCanPurchaseGradeList(Guid? memberID);

        /// <summary>
        /// 手机绑定
        /// </summary>
        /// <param name="mobilephoneno"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        bool BindingMobilePhone(string mobilephoneno, string openId);

        /// <summary>
        /// 核销密码设置
        /// </summary>
        /// <param name="password"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        bool VerificationCodeSet(string password, string openId);

        /// <summary>
        /// 手动新增会员
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Member ManualAddMember(AddMemberParam param);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchDelete(Guid[] ids);

        /// <summary>
        /// 批量手动新增会员
        /// </summary>
        /// <param name="addparam"></param>
        /// <returns></returns>
        bool BatchManualAddMember(List<AddMemberParam> addparam);

        /// <summary>
        /// 导入会员数据
        /// </summary>
        /// <param name="members"></param>
        /// <returns></returns>
        bool ImportMember(IEnumerable<AddMemberParam> members);

        /// <summary>
        /// 设置股东分红
        /// </summary>
        /// <param name="id"></param>
        /// <param name="consumePointRate"></param>
        /// <param name="discountRate"></param>
        /// <returns></returns>
        bool SetStockholder(Guid id, decimal consumePointRate, decimal discountRate);

        /// <summary>
        /// 取消股东分红
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CancelStockholder(Guid id);
    }
}

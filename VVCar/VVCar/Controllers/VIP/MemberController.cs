using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using VVCar.Common;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Dtos;
using YEF.Core.Export;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 会员
    /// </summary>
    [RoutePrefix("api/Member")]
    public class MemberController : BaseApiController
    {
        #region ctor.

        /// <summary>
        /// 会员
        /// </summary>
        /// <param name="memberService"></param>
        public MemberController(IMemberService memberService)
        {
            MemberService = memberService;
        }

        #endregion

        #region properties

        IMemberService MemberService { get; set; }

        #endregion

        /// <summary>
        /// 删除会员
        /// </summary>
        /// <param name="id">会员ID</param>
        /// <returns></returns>
        [HttpDelete]
        public JsonActionResult<bool> DeleteMember(Guid id)
        {
            return SafeExecute(() =>
            {
                return MemberService.Delete(id);
            });
        }

        /// <summary>
        /// 会员注册
        /// </summary>
        /// <param name="registerDto">The register dto.</param>
        /// <returns></returns>
        [HttpPost, Route("Register"), AllowAnonymous]
        public JsonActionResult<MemberCardDto> Register(MemberRegisterDto registerDto)
        {
            return SafeExecute(() =>
            {
                return MemberService.Register(registerDto);
            });
        }

        /// <summary>
        /// 更新会员
        /// </summary>
        /// <param name="member">会员</param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> UpdateMember(Member member)
        {
            return SafeExecute(() => MemberService.Update(member));
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="member">会员</param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<Member> AddMember(Member member)
        {
            return SafeExecute(() => MemberService.Add(member));
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpDelete, Route("BatchDelete")]
        public JsonActionResult<bool> BatchDelete(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return MemberService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 获取会员数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonActionResult<Member> GetMember(Guid id)
        {
            return SafeExecute(() =>
            {
                return this.MemberService.Get(id);
            });
        }

        /// <summary>
        /// 查询会员
        /// </summary>
        /// <param name="filter">部门过滤条件</param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<MemberDto> Search([FromUri]MemberFilter filter)
        {
            return SafeGetPagedData<MemberDto>((result) =>
            {
                if (!ModelState.IsValid)//表示没有过滤参数成功匹配，判定为错误请求。
                {
                    throw new DomainException("查询参数错误。");
                }
                var pagedData = this.MemberService.Search(filter);
                result.Data = pagedData.Items;
                result.TotalCount = pagedData.TotalCount;
            });
        }

        /// <summary>
        /// 根据微信OpenID获取会员信息
        /// </summary>
        /// <param name="openID"></param>
        /// <returns></returns>
        [HttpGet, Route("GetMemberInfoByWeChat"), AllowAnonymous]
        public JsonActionResult<MemberCardDto> GetMemberInfoByWeChat([FromUri]GetMemberInfoByWeChatDto getMemberInfoByWeChatDto)
        {
            return SafeExecute(() =>
            {
                return MemberService.GetMemberInfoByWeChat(getMemberInfoByWeChatDto.openID, getMemberInfoByWeChatDto.isagentdept);
            });
        }

        ///// <summary>
        ///// 获取会员分析-用户明细分页数据
        ///// </summary>
        ///// <param name="filter">分页参数</param>
        ///// <returns></returns>
        //[HttpGet, Route("GetMemberDetail")]
        //[AllowAnonymous]
        //public PagedActionResult<MemberDetailDto> GetMemberDetail([FromUri]MemberDetailFilter filter)
        //{
        //    return SafeGetPagedData<MemberDetailDto>((result) =>
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException("查询参数错误。");
        //        }
        //        var pagedData = this.MemberService.GetMemberDetail(filter);
        //        result.Data = pagedData.Items;
        //        result.TotalCount = pagedData.TotalCount;
        //    });
        //}

        ///// <summary>
        ///// 会员分析->人数分析->获取用户明细
        ///// </summary>
        ///// <param name="filter"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("GetMemberDetailForConsumeAnalysis")]
        //public PagedActionResult<MemberDetailDto> GetMemberDetailForConsumeAnalysis([FromUri]MemberDetailFilter filter)
        //{
        //    return SafeGetPagedData<MemberDetailDto>((result) =>
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            throw new DomainException("参数错误.");
        //        }
        //        var totalCount = 0;
        //        var data = this.MemberService.GetMemberDetailForConsumeAnalysis(filter, ref totalCount);
        //        result.TotalCount = totalCount;
        //        result.Data = data;
        //    });
        //}

        ///// <summary>
        ///// 获取会员分析-新增会员
        ///// </summary>
        ///// <param name="filter">过滤参数</param>
        ///// <returns></returns>
        //[HttpGet, Route("GetNewMemberCount")]
        //[AllowAnonymous]
        //public JsonActionResult<IEnumerable<NewMemberCountOfDateRange>> GetNewMemberCount([FromUri]NewMemberFilter filter)
        //{
        //    return SafeExecute(() =>
        //    {
        //        if (!ModelState.IsValid)

        //        {
        //            throw new DomainException("查询参数错误。");
        //        }
        //        return MemberService.GetNewMemberCount(filter);
        //    });
        //}

        /// <summary>
        /// 获取基本信息
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpGet, Route("GetBaseInfo")]
        public JsonActionResult<MemberBaseInfoDto> GetBaseInfo(Guid memberId)
        {
            return SafeExecute(() => MemberService.GetBaseInfo(memberId));
        }

        /// <summary>
        /// 挂失
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        [HttpPost, Route("ReportLoss")]
        public JsonActionResult<bool> ReportLoss([FromBody]string cardNumber)
        {
            return SafeExecute(() => MemberService.ReportLoss(cardNumber));
        }

        /// <summary>
        /// 取消挂失
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        [HttpPost, Route("CancelLoss")]
        public JsonActionResult<bool> CancelLoss([FromBody]string cardNumber)
        {
            return SafeExecute(() => MemberService.CancelLoss(cardNumber));
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        [HttpPost, Route("ResetPassword/{memberID}")]
        public JsonActionResult<bool> ResetPassword(Guid memberID)
        {
            return SafeExecute(() => MemberService.ResetPassword(memberID));
        }

        ///// <summary>
        ///// 换卡
        ///// </summary>
        ///// <param name="changeCardDto">换卡请求信息</param>
        ///// <returns></returns>
        //[HttpPost, Route("ChangeCard")]
        //public JsonActionResult<bool> ChangeCard(ChangeCardDto changeCardDto)
        //{
        //    return SafeExecute(() =>
        //    {
        //        if (!ModelState.IsValid)//表示没有过滤参数成功匹配，判定为错误请求。
        //        {
        //            throw new DomainException("查询参数错误。");
        //        }
        //        return MemberService.ChangeCard(changeCardDto);
        //    });
        //}

        /// <summary>
        /// 导出会员
        /// </summary>
        /// <param name="filter">会员过滤条件</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ExportMember")]
        public JsonActionResult<string> ExportMember([FromUri] MemberFilter filter)
        {
            return SafeExecute(() =>
            {
                if (!ModelState.IsValid)//表示没有过滤参数成功匹配，判定为错误请求。
                {
                    throw new DomainException("查询参数错误。");
                }
                filter.Start = null;
                filter.Limit = null;
                var pagedData = this.MemberService.Search(filter);
                //{ header: "会员卡号",  dataIndex: "CardNumber"       , flex: 1 },
                //{ header: "卡片类型",  dataIndex: "CardType"         , flex: 1 },
                //{ header: "姓名"    ,  dataIndex: "Name"             , flex: 1 },
                //{ header: "手机号码",  dataIndex: "MobilePhoneNo"    , flex: 1 },
                //{ header: "会员状态",  dataIndex: "Status"           , flex: 1 },
                //{ header: "注册时间",  dataIndex: "CreatedDate"      , xtype: "datecolumn", format: "Y-m-d H:i:s", flex: 1 },
                ////{ header"注册门店",  dataIndex: "CreatedDepartment", flex: 1 },
                //{ header: "余额（元）", dataIndex:"CardBalance"      , flex: 1, xtype: "numbercolumn" }
                var exporter = new ExportHelper(new[]
                {
                    new ExportInfo("MemberGroup","分组"),
                    new ExportInfo("Name", "姓名"),
                    new ExportInfo("Sex", "性别"),
                    new ExportInfo("MobilePhoneNo", "手机号码"),
                    new ExportInfo("PhoneLocation", "归属地"),
                    new ExportInfo("PlateList", "车牌号"),
                    new ExportInfo("Point","剩余积分"),
                    new ExportInfo("Status","会员状态"),
                    new ExportInfo("InsuranceExpirationDate", "保险到期时间"),
                    new ExportInfo("CreatedDate","注册时间"),
                    //new ExportInfo("CardNumber","会员卡号"),
                    //new ExportInfo("CardTypeDesc","卡片类型"),
                    //new ExportInfo("PhoneLocation","归属地"),
                    //new ExportInfo("MemberGradeName","会员等级"),
                    //new ExportInfo("OwnerDepartment","所属门店"),
                    //new ExportInfo("CardBalance","余额（元）"),
                    //new ExportInfo("WeChatOpenID","OpenId"),
                });
                return exporter.Export(pagedData.Items.ToList(), "会员信息");
            });
        }

        /// <summary>
        /// 会员导入模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ImportMemberTemplate")]
        public JsonActionResult<string> ImportMemberTemplate()
        {
            return SafeExecute(() =>
            {
                var templateData = new List<AddMemberParam>();
                var exportInfos = new[]
                {
                    new ExportInfo("Name", "姓名"),
                    new ExportInfo("Sex", "性别"),
                    new ExportInfo("Birthday", "出生年月"),
                    new ExportInfo("MobilePhoneNo", "手机号码"),
                    new ExportInfo("PlateNumber", "车牌号"),
                    new ExportInfo("InsuranceExpirationDate", "保险到期"),
                };
                var eh = new ExportHelper(exportInfos);
                return eh.ImportMemberTemplate("会员导入模板");
            });
        }

        /// <summary>
        /// 导入会员数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet, Route("ImportMember")]
        public JsonActionResult<bool> ImportMember(string fileName)
        {
            return SafeExecute(() =>
            {
                var targetDir = Path.Combine(AppContext.PathInfo.AppDataPath, "Upload/Excel/Member");
                string targetPath = Path.Combine(targetDir, fileName);

                var excelFieldInfos = new[]
                {
                    new ExcelFieldInfo(0, "Name", "姓名"),
                    new ExcelFieldInfo(1, "Sex", "性别"),
                    new ExcelFieldInfo(2, "Birthday", "出生年月", true),
                    new ExcelFieldInfo(3, "MobilePhoneNo", "手机号码"),
                    new ExcelFieldInfo(4, "PlateNumber", "车牌号", true),
                    new ExcelFieldInfo(5, "InsuranceExpirationDate", "保险到期", true)
                };
                var data = ExcelHelper.ImportFromExcel<AddMemberParam>(targetPath, excelFieldInfos);
                if (data == null || data.Count < 1)
                    throw new DomainException("没有可以导入的数据");
                return MemberService.ImportMember(data);
            });
        }

        /// <summary>
        /// 获取号码归属地
        /// </summary>
        /// <param name="phoneNumber">手机号码</param>
        /// <returns></returns>
        [HttpGet, Route("GetPhoneLoaction/{phoneNumber}")]
        public JsonActionResult<string> GetPhoneLoaction(string phoneNumber)
        {
            return SafeExecute(() =>
            {
                return MemberService.GetPhoneLoaction(phoneNumber);
            });
        }

        ///// <summary>
        ///// 会员总计信息
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("Statistic")]
        //public JsonActionResult<StatisticDto> Statistic()
        //{
        //    return SafeExecute(() => MemberService.Statistic());
        //}

        /// <summary>
        /// 移动会员分组
        /// </summary>
        /// <param name="changeDto"></param>
        /// <returns></returns>
        [HttpPut, Route("ChangeMemberGroup")]
        public JsonActionResult<bool> ChangeMemberGroup(ChangeMemberGroupDto changeDto)
        {
            return SafeExecute(() => MemberService.ChangeMemberGroup(changeDto));
        }

        /// <summary>
        /// 会员积分调整
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("AdjustMemberPoint")]
        public JsonActionResult<bool> AdjustMemberPoint([FromUri]AdjustMemberPointFilter filter)
        {
            return SafeExecute(() =>
            {
                return MemberService.AdjustMemberPoint(filter.MemberID, filter.PointType, filter.AdjustPoints);
            });
        }

        /// <summary>
        /// 核销密码设置
        /// </summary>
        /// <param name="password"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        [HttpGet, Route("VerificationCodeSet"), AllowAnonymous]
        public JsonActionResult<bool> VerificationCodeSet(string password, string openId)
        {
            return SafeExecute(() =>
            {
                return MemberService.VerificationCodeSet(password, openId);
            });
        }

        /// <summary>
        /// 手机绑定
        /// </summary>
        /// <param name="mobilephoneno"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        [HttpGet, Route("BindingMobilePhone"), AllowAnonymous]
        public JsonActionResult<bool> BindingMobilePhone(string mobilephoneno, string openId)
        {
            return SafeExecute(() =>
            {
                return MemberService.BindingMobilePhone(mobilephoneno, openId);
            });
        }

        /// <summary>
        /// 手动新增会员
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost, Route("ManualAddMember")]
        public JsonActionResult<Member> ManualAddMember(AddMemberParam param)
        {
            return SafeExecute(() =>
            {
                return MemberService.ManualAddMember(param);
            });
        }

        /// <summary>
        /// 批量手动新增会员
        /// </summary>
        /// <param name="addparam"></param>
        /// <returns></returns>
        [HttpPost, Route("BatchManualAddMember")]
        public JsonActionResult<bool> BatchManualAddMember(List<AddMemberParam> addparam)
        {
            return SafeExecute(() =>
            {
                return MemberService.BatchManualAddMember(addparam);
            });
        }

        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="memberLoginDto"></param>
        /// <returns></returns>
        [HttpPost, Route("MemberLogin"), AllowAnonymous]
        public JsonActionResult<MemberCardDto> MemberLogin(MemberLoginDto memberLoginDto)
        {
            return SafeExecute(() =>
            {
                return MemberService.MemberLogin(memberLoginDto);
            });
        }
    }
}

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.Common;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Dtos;
using YEF.Core.Export;
using YEF.Utility;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 会员卡
    /// </summary>
    [RoutePrefix("api/MemberCard")]
    public class MemberCardController : BaseApiController
    {
        #region ctor.

        /// <summary>
        /// 会员卡
        /// </summary>
        /// <param name="deptService"></param>
        public MemberCardController(IMemberCardService memberCardService)
        {
            this.MemberCardService = memberCardService;
        }

        #endregion ctor.

        #region properties

        private IMemberCardService MemberCardService { get; set; }

        #endregion properties

        /// <summary>
        /// 删除会员卡
        /// </summary>
        /// <param name="id">会员ID</param>
        /// <returns></returns>
        [HttpDelete]
        public JsonActionResult<bool> DeleteMemberCard(Guid id)
        {
            return SafeExecute(() =>
            {
                return this.MemberCardService.Delete(id);
            });
        }

        /// <summary>
        /// 更新会员卡
        /// </summary>
        /// <param name="department">会员</param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> UpdateMemberCard(MemberCard memberCard)
        {
            return SafeExecute(() =>
            {
                return this.MemberCardService.Update(memberCard);
            });
        }

        /// <summary>
        /// 获取会员卡数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonActionResult<MemberCard> GetMemberCard(Guid id)
        {
            return SafeExecute(() =>
            {
                return this.MemberCardService.Get(id);
            });
        }

        /// <summary>
        /// 调整余额
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AdjustBalance")]
        public JsonActionResult<bool> AdjustBalance(AdjustBalanceDto entity)
        {
            return SafeExecute(() => MemberCardService.AdjustBalance(entity));
        }

        /// <summary>
        /// 获取卡号所属的卡片类型
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet, Route("GetCardType/{code}")]
        public JsonActionResult<MemberCardTypeDto> GetCardType(string code)
        {
            return SafeExecute(() => MemberCardService.GetCardType(code));
        }

        /// <summary>
        /// 验证卡号有效性
        /// </summary>
        /// <param name="memberCard"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("VerifyCode")]
        public JsonActionResult<bool> VerifyCode(MemberCardFilter memberCard)
        {
            return SafeExecute(() => MemberCardService.VerifyCode(memberCard));
        }

        /// <summary>
        /// 根据卡号或者手机号码获取卡信息
        /// </summary>
        /// <param name="number">会员卡号或者手机号码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCardByNumber"), AllowAnonymous]
        public JsonActionResult<MemberCardDto> GetCardByNumber(string number)
        {
            return SafeExecute(() =>
            {
                if (string.IsNullOrEmpty(number))
                    throw new DomainException("参数错误");
                if (number.Length > 8)
                {
                    var timespan = new TimeSpan(long.Parse(number.Substring(9)));
                    var minutes = (new TimeSpan(DateTime.Now.Ticks) - timespan).Minutes;
                    if (minutes >= 2)
                        throw new DomainException("二维码已过期");
                    number = number.Substring(0, 8);
                }
                return MemberCardService.GetCardInfoByNumber(number);
            });
        }

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="rechargeInfo"></param>
        /// <returns></returns>
        [HttpPost, Route("Recharge")]
        public JsonActionResult<CardTradeResultDto> Recharge(RechargeInfoDto rechargeInfo)
        {
            return SafeExecute(() => MemberCardService.Recharge(rechargeInfo, ETradeSource.Portal));
        }

        /// <summary>
        /// 查询会员卡
        /// </summary>
        /// <param name="filter">部门过滤条件</param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<MemberCard> Search([FromUri]MemberCardFilter filter)
        {
            return SafeGetPagedData<MemberCard>((result) =>
            {
                if (!ModelState.IsValid) //表示没有过滤参数成功匹配，判定为错误请求。
                {
                    throw new DomainException("查询参数错误。");
                }
                if (filter != null && filter.IsGenerate != null && filter.IsGenerate.Value)
                {
                    result.Data = MemberCardService.PreGenerate(filter);
                    result.TotalCount = result.Data.Count();
                }
                else
                {
                    var pagedData = MemberCardService.QueryData(filter);
                    result.Data = pagedData.Items;
                    result.TotalCount = pagedData.TotalCount;
                }
            });
        }

        ///// <summary>
        ///// 保存生成的卡号
        ///// </summary>
        ///// <param name="entities"></param>
        ///// <returns></returns>
        //[HttpPost, Route("saveGenerate")]
        //public JsonActionResult<SaveGenerateCardResultModel> SaveGenerate(IEnumerable<MemberCard> entities)
        //{
        //    return SafeExecute<SaveGenerateCardResultModel>(() =>
        //    {
        //        if (entities == null)
        //            throw new DomainException("会员卡信息集合为空。");
        //        var result = new SaveGenerateCardResultModel();
        //        result.BatchCode = MemberCardService.BatchSave(entities);
        //        var membeCardExportDto = MemberCardService.FillGenerateEntities(entities);
        //        result.DownloadLink = ExecuteExport(membeCardExportDto);
        //        return result;
        //    });
        //}

        //private static string ExecuteExport(IEnumerable<MemberCardExportDto> entities)
        //{
        //    var exportHelper = new ExportHelper(new[]
        //    {
        //        new ExportInfo("Code", "卡片编号"),
        //        new ExportInfo("CardTypeDesc", "卡片类型"),
        //        new ExportInfo("Status", "卡片状态"),
        //        new ExportInfo("MemberGroup", "会员分组"),
        //        new ExportInfo("MemberGrade","会员等级"),
        //        new ExportInfo("VerifyCode", "验证码"),
        //        new ExportInfo("CardBalance", "余额"),
        //        new ExportInfo("BatchCode", "批次代码"),
        //        new ExportInfo("CreatedDate", "生成日期"),
        //        new ExportInfo("EffectiveDate", "生效日期"),
        //        new ExportInfo("SecurityCode", "安全卡号"),
        //        new ExportInfo("URL", "URL"),
        //        new ExportInfo("Remark","备注"),
        //    });
        //    exportHelper.OnSheetCreated += (sheet) =>
        //    {
        //        sheet.SetColumnWidth(exportHelper.ExportInfoCount, 256 * 50);
        //    };
        //    exportHelper.OnRowFilled += (book, row, entity) =>
        //    {
        //        dynamic mc = entity;
        //        if (mc.Status == ECardStatus.UnActivate.GetDescription())
        //        {
        //            row.HeightInPoints = 280;
        //            var targetUrl = string.Empty;
        //            if (mc.CardTypeDesc == "礼品卡")
        //                targetUrl = $"{AppContext.Settings.SiteDomain}/Mobile/GiftCardBinding?mch={AppContext.CurrentSession.CompanyCode}&CardCode={mc.Code}";
        //            else
        //                targetUrl = string.Format("{0}/JH.aspx?mch={1}&c={2}&v={3}",
        //                 AppContext.Settings.OnlinePayService, AppContext.CurrentSession.CompanyCode,
        //                 mc.Code, mc.VerifyCode);
        //            AddPicture(QrHelper.ImageBuffer(targetUrl), row.Sheet, book, row.Cells.Count(), row.RowNum);
        //        }
        //    };
        //    var exportData = entities.Select(s => new
        //    {
        //        s.Code,
        //        CardTypeDesc = s.CardTypeDesc,
        //        Status = s.Status.GetDescription(),
        //        s.MemberGroup,
        //        s.MemberGrade,
        //        s.VerifyCode,
        //        s.CardBalance,
        //        s.BatchCode,
        //        s.CreatedDate,
        //        s.EffectiveDate,
        //        s.SecurityCode,
        //        s.URL,
        //        s.Remark,
        //    });
        //    return exportHelper.Export(exportData.ToList());
        //}

        //private static void AddPicture(byte[] buffer, ISheet sheeet, HSSFWorkbook workBook, int col1, int row1)
        //{
        //    var pictureIndex = workBook.AddPicture(buffer, PictureType.JPEG);

        //    var patriarch = (HSSFPatriarch)sheeet.CreateDrawingPatriarch();
        //    var anchor = new HSSFClientAnchor(0, 0, 0, 0, col1, row1, col1, row1);
        //    var picture = patriarch.CreatePicture(anchor, pictureIndex);
        //    picture.Resize(0.8);
        //}

        //private static byte[] GetImageBytes(Image image)
        //{
        //    var ms = new MemoryStream();
        //    image.Save(ms, ImageFormat.Jpeg);
        //    ms.Position = 0;
        //    var buffer = new byte[ms.Length];
        //    ms.Read(buffer, 0, (int)ms.Length);
        //    return buffer;
        //}

        //[HttpPost]
        //[Route("exportMemberCard")]
        //public JsonActionResult<string> ExportMemberCard(MemberCardFilter filter)
        //{
        //    return SafeExecute(() =>
        //    {
        //        filter.Start = 0;
        //        filter.Limit = int.MaxValue;
        //        var pagedData = MemberCardService.QueryData(filter).Items;
        //        if (pagedData == null)
        //            throw new DomainException("会员卡信息集合为空。");
        //        var membeCardExportDto = MemberCardService.FillGenerateEntities(pagedData);
        //        return ExecuteExport(membeCardExportDto);
        //    });
        //}

        ///// <summary>
        ///// 激活会员卡
        ///// </summary>
        ///// <param name="info"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("Activate")]
        //public JsonActionResult<bool> Activate(MembercardActivateInfo info)
        //{
        //    return SafeExecute(() => MemberCardService.Activate(info, MMS.Domain.Enums.EClientType.Portal));
        //}

        ///// <summary>
        ///// 消费
        ///// </summary>
        ///// <param name="consumeInfo"></param>
        ///// <returns></returns>
        //[HttpPost, Route("Consume")]
        //public JsonActionResult<CardTradeResultDto> Consume(ConsumeInfoDto consumeInfo)
        //{
        //    return SafeExecute(() => MemberCardService.Consume(consumeInfo, MMS.Domain.Enums.ETradeSource.Portal));
        //}

        ///// <summary>
        ///// 礼品卡批量激活
        ///// </summary>
        ///// <param name="BatchActivateInfo">The batch activate information.</param>
        ///// <returns></returns>
        //[HttpPost, Route("GiftCardBatchActivate")]
        //public JsonActionResult<bool> GiftCardBatchActivate(GiftCardBatchActivateDto BatchActivateInfo)
        //{
        //    return SafeExecute(() =>
        //    {
        //        return MemberCardService.GiftCardBatchActivate(BatchActivateInfo);
        //    });
        //}

        ///// <summary>
        ///// 批量修改备注
        ///// </summary>
        ///// <param name="modifyInfo">The modify information.</param>
        ///// <returns></returns>
        //[HttpPost, Route("BatchModifyRemark")]
        //public JsonActionResult<bool> BatchModifyRemark(BatchModifyRemarkDto modifyInfo)
        //{
        //    return SafeExecute(() =>
        //    {
        //        return MemberCardService.BatchModifyRemark(modifyInfo);
        //    });
        //}
    }
}

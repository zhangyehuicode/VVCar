using System;
using System.Linq;
using System.Web.Http;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using VVCar.Common;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Dtos;
using YEF.Core.Export;

namespace VVCar.Controllers.Api
{
    /// <summary>
    ///  商户
    /// </summary>
    [RoutePrefix("api/Merchant")]
    public class MerchantController : BaseApiController
    {
        public MerchantController(IMerchantService merchantService)
        {
            MerchantService = merchantService;
        }

        IMerchantService MerchantService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<Merchant> Add(Merchant entity)
        {
            return SafeExecute(() =>
            {
                return MerchantService.Add(entity);
            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public JsonActionResult<bool> Delete(Guid id)
        {
            return SafeExecute(() =>
            {
                return MerchantService.Delete(id);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(Merchant entity)
        {
            return SafeExecute(() =>
            {
                return MerchantService.Update(entity);
            });
        }

        /// <summary>
        /// 激活商户
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost, Route("activateMerchant")]
        public JsonActionResult<bool> ActivateMerchant(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return MerchantService.ActivateMerchant(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 冻结商户
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost, Route("freezeMerchant")]
        public JsonActionResult<bool> FreezeMerchant(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return MerchantService.FreezeMerchant(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<Merchant> Search([FromUri]MerchantFilter filter)
        {
            return SafeGetPagedData<Merchant>((result) =>
            {
                var totalCount = 0;
                var data = MerchantService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 获取子商户
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetSubMerchants"), AllowAnonymous]
        public PagedActionResult<Merchant> GetSubMerchants()
        {
            return SafeGetPagedData<Merchant>((result) =>
            {
                result.Data = MerchantService.GetSubMerchants();
                result.TotalCount = result.Data.Count();
            });
        }

        /// <summary>
        /// 导出商户
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("ExportMerchant")]
        public JsonActionResult<string> ExportMerchant([FromUri]MerchantFilter filter)
        {
            return SafeExecute(() =>
            {
                filter.Start = null;
                filter.Limit = null;
                var totalCount = 0;
                var data = this.MerchantService.Search(filter, out totalCount);
                var exporter = new ExportHelper(new[]
                {
                    new ExportInfo("Code", "商户号"),
                    new ExportInfo("Name","名称"),
                    new ExportInfo("Status","商户状态"),
                    new ExportInfo("Email","注册邮箱"),
                    new ExportInfo("LegalPerson","法人(负责人)"),
                    new ExportInfo("IDNumber","法人身份证编号"),
                    new ExportInfo("MobilePhoneNo","联系电话"),
                    new ExportInfo("Bank","开户行"),
                    new ExportInfo("BankCard","账号"),
                    new ExportInfo("CompanyAddress","公司地址"),
                });

                return exporter.Export(data.ToList(), "商户信息统计"); ;
            });
        }
    }
}

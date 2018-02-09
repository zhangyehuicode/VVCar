using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Api
{
    /// <summary>
    /// 数据字典
    /// </summary>
    [RoutePrefix("api/DataDictValue")]
    public class DataDictValueController : BaseApiController
    {
        #region ctor.

        /// <summary>
        /// 数据字典
        /// </summary>
        /// <param name="dictValueService"></param>
        public DataDictValueController(IDataDictValueService dictValueService)
        {
            this.DictValueService = dictValueService;
        }

        #endregion

        #region properties

        /// <summary>
        /// 数据字典值 领域服务
        /// </summary>
        IDataDictValueService DictValueService { get; set; }

        #endregion

        /// <summary>
        /// 新增数据字典值
        /// </summary>
        /// <param name="newDictValue">数据字典值</param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<DataDictValue> AddDictValue(DataDictValue newDictValue)
        {
            return SafeExecute(() =>
            {
                return this.DictValueService.Add(newDictValue);
            });
        }

        /// <summary>
        /// 更新数据字典值
        /// </summary>
        /// <param name="dictValue">数据字典值</param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> UpdateDictValue(DataDictValue dictValue)
        {
            return SafeExecute(() =>
            {
                return this.DictValueService.Update(dictValue);
            });
        }

        /// <summary>
        /// 根据字典类型获取可用的字典值
        /// </summary>
        /// <param name="dictType">字典类型</param>
        /// <returns></returns>
        [HttpGet]
        public JsonActionResult<IEnumerable<DataDictValue>> GetDictValuesByType(string dictType)
        {
            return SafeExecute(() =>
            {
                return this.DictValueService.GetDictValuesByType(dictType);
            });
        }

        /// <summary>
        /// 根据字典类型获取字典值
        /// </summary>
        /// <param name="dictType">字典类型</param>
        /// <returns></returns>
        [HttpGet, Route("AllData")]
        public JsonActionResult<IEnumerable<DataDictValue>> GetDictValues(string dictType)
        {
            return SafeExecute<IEnumerable<DataDictValue>>(() =>
            {
                if (string.IsNullOrEmpty(dictType))
                    return null;
                return this.DictValueService.Query(t => t.DictType == dictType).OrderBy(t => t.Index).ToArray();
            });
        }
    }
}

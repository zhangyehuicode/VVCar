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
    /// 数据字典类型
    /// </summary>
    [RoutePrefix("api/DataDictType")]
    public class DataDictTypeController : BaseApiController
    {
        #region ctor.

        /// <summary>
        /// 数据字典类型
        /// </summary>
        /// <param name="dictTypeService"></param>
        public DataDictTypeController(IDataDictTypeService dictTypeService)
        {
            this.DictTypeService = dictTypeService;
        }

        #endregion

        #region properties

        IDataDictTypeService DictTypeService { get; set; }

        #endregion

        /// <summary>
        /// 获取数据字典类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonActionResult<IEnumerable<DataDictType>> Get()
        {
            return SafeExecute<IEnumerable<DataDictType>>(() =>
            {
                return this.DictTypeService.Query(null).OrderBy(t => t.Index).ToArray();
            });
        }

        /// <summary>
        /// 更新数据字典类型
        /// </summary>
        /// <param name="dictType"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(DataDictType dictType)
        {
            return SafeExecute(() =>
            {
                return this.DictTypeService.Update(dictType);
            });
        }
    }
}

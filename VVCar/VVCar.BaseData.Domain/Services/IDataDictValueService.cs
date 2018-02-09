using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.BaseData.Domain.Services
{
    /// <summary>
    /// 数据字典值领域服务
    /// </summary>
    public partial interface IDataDictValueService : IDomainService<IRepository<DataDictValue>, DataDictValue, Guid>
    {
        /// <summary>
        /// 根据字典类型获取字典值
        /// </summary>
        /// <param name="dictType">数据字典类型</param>
        /// <returns></returns>
        IEnumerable<DataDictValue> GetDictValuesByType(string dictType);
    }
}

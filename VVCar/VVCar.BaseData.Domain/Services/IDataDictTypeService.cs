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
    public partial interface IDataDictTypeService : IDomainService<IRepository<DataDictType>, DataDictType, Guid>
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YEF.Core.Data
{
    /// <summary>
    /// Guid主键实体仓储接口
    /// </summary>
    /// <typeparam name="TEntity">主键类型为Guid的实体类型</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, Guid> where TEntity : NormalEntityBase<Guid>, new()
    {
    }
}

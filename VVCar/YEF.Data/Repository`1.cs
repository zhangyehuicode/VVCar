using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace YEF.Data
{
    /// <summary>
    /// Guid主键实体的仓储实现
    /// </summary>
    /// <typeparam name="TEntity">主键类型为Guid的实体类型</typeparam>
    public class Repository<TEntity> : Repository<TEntity, Guid>, IRepository<TEntity>, IRepositoryAsync<TEntity>
        where TEntity : EntityBase<Guid>, new()
    {
        public Repository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}

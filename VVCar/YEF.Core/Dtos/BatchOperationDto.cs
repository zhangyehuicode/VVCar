using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YEF.Core.Dtos
{
    /// <summary>
    /// 批量操作对象DTO
    /// </summary>
    public class BatchOperationDto<T>
    {
        /// <summary>
        /// 批量操作数据集合
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// 操作标识
        /// </summary>
        public string Mark { get; set; }
    }

    /// <summary>
    /// 批量操作DTO,Guid重载
    /// </summary>
    public class BatchOperationDto : BatchOperationDto<Guid>
    {
        /// <summary>
        /// 批量操作主键ID列表
        /// </summary>
        public IEnumerable<Guid> IdList
        {
            get
            {
                return this.Data;
            }
            set
            {
                this.Data = value;
            }
        }
    }
}

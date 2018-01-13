
namespace YEF.Core.Dtos
{
    /// <summary>
    /// 分页条件
    /// </summary>
    public class BasePageFilter : BaseFilter
    {
        /// <summary>
        /// 获取第Start+1页的数据
        /// </summary>
        public int? Start { get; set; }

        /// <summary>
        /// 每一个分页获取的条数
        /// </summary>
        public int? Limit { get; set; }

    }
}

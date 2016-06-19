namespace JuCheap.Core.Models.Filters
{
    /// <summary>
    /// 基本过滤器
    /// </summary>
    public class BaseFilter
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 每页显示的数据量
        /// </summary>
        public int rows { get; set; }

        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string keywords { get; set; }
    }
}

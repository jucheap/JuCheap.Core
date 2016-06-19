using System.Collections.Generic;
using JuCheap.Core.Infrastructure.Extentions;

namespace JuCheap.Core.Infrastructure
{
    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public PagedResult()
        {
            rows = new List<T>();
        }

        /// <summary>
        /// ctor with params
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示数量</param>
        public PagedResult(int pageIndex, int pageSize)
        {
            page = pageIndex;
            pagesize = pageSize;
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int records { set; get; }
        /// <summary>
        /// 当前页的所有项
        /// </summary>
        public IList<T> rows { set; get; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int page { set; get; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int pagesize { set; get; }
        /// <summary>
        /// 页总数
        /// </summary>
        public int total { get { return records.CeilingDivide(pagesize); } }
    }
}

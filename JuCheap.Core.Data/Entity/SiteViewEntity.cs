using System;

namespace JuCheap.Core.Data.Entity
{
    /// <summary>
    /// 访问量统计
    /// </summary>
    public class SiteViewEntity : BaseEntity
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Day { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        public int Number { get; set; }
    }
}

using System;

namespace JuCheap.Core.Data.Entity
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SystemConfigEntity : BaseEntity
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// 数据初始化是否完成
        /// </summary>
        public bool IsDataInited { get; set; }

        /// <summary>
        /// 数据初始化时间
        /// </summary>
        public DateTime DataInitedDate { get; set; }
    }
}

using System;

namespace JuCheap.Core.Data.Entity
{
    /// <summary>
    /// 省市区实体
    /// </summary>
    public class AreaEntity : BaseEntity
    {
        /// <summary>
        /// 父级Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 全拼
        /// </summary>
        public string FullSpelling { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>
        public string SimpleSpelling { get; set; }

        /// <summary>
        /// 路劲码
        /// </summary>
        public string PathCode { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplaySequence { get; set; }
    }
}

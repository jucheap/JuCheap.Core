using System;

namespace JuCheap.Core.Models
{
    /// <summary>
    /// ztree模型
    /// </summary>
    public class TreeDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// 父类ID
        /// </summary>
        public Guid? pId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        public bool open { get; set; }

        /// <summary>
        /// 是否是父节点
        /// </summary>
        public bool isParent { get; set; }
    }
}

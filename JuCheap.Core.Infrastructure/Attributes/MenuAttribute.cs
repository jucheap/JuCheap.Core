using System;

namespace JuCheap.Core.Infrastructure.Attributes
{
    /// <summary>
    /// 菜单Attribute
    /// </summary>
    public class MenuAttribute : Attribute
    {
        /// <summary>
        /// 菜单唯一标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 父类菜单唯一标识
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 菜单名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public string Order { get; set; }
    }
}

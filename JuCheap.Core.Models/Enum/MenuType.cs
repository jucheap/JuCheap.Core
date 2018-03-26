using System.ComponentModel;

namespace JuCheap.Core.Models.Enum
{
    /// <summary>
    /// 菜单类型
    /// </summary>
    public enum MenuType : byte
    {
        /// <summary>
        /// 模块
        /// </summary>
        [Description("模块")]
        Module = 1,

        /// <summary>
        /// 菜单
        /// </summary>
        [Description("菜单")]
        Menu = 2,

        /// <summary>
        /// 操作
        /// </summary>
        [Description("操作")]
        Action = 3
    }
}

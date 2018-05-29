using System.ComponentModel;

namespace JuCheap.Core.Infrastructure.Enums
{
    /// <summary>
    /// 控件类型
    /// </summary>
    public enum ControlType
    {
        /// <summary>
        /// 单行文本框
        /// </summary>
        [Description("单行文本框")]
        SingleText = 1,
        /// <summary>
        /// 多行文本框
        /// </summary>
        [Description("多行文本框")]
        MultiText = 2,
        /// <summary>
        /// 下拉框
        /// </summary>
        [Description("下拉框")]
        Select = 3,
        /// <summary>
        /// 日期
        /// </summary>
        [Description("日期")]
        Date = 4,
        /// <summary>
        /// 日期时间
        /// </summary>
        [Description("日期时间")]
        DateTime = 5
    }
}

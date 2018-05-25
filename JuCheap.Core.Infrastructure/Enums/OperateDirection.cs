using System.ComponentModel;

namespace JuCheap.Core.Infrastructure.Enums
{
    /// <summary>
    /// 操作方向
    /// </summary>
    public enum OperateDirection
    {
        /// <summary>
        /// 上一步
        /// </summary>
        [Description("退回到上一步")]
        Preview = -1,
        /// <summary>
        /// 退回到发起人
        /// </summary>
        [Description("退回到发起人")]
        ReturnToStarter = 0,
        /// <summary>
        /// 下一步
        /// </summary>
        [Description("下一步")]
        Next = 1,
        /// <summary>
        /// 结束
        /// </summary>
        [Description("结束")]
        Finish = 99
    }
}

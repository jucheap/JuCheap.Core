namespace JuCheap.Core.Infrastructure.Enums
{
    /// <summary>
    /// 操作方向
    /// </summary>
    public enum OperateDirection
    {
        /// <summary>
        /// 退回到上一步
        /// </summary>
        Preview = -1,
        /// <summary>
        /// 退回到发起人
        /// </summary>
        ReturnToStarter = 0,
        /// <summary>
        /// 下一步
        /// </summary>
        Next = 1,
        /// <summary>
        /// 结束
        /// </summary>
        Finish = 99
    }
}

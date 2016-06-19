namespace JuCheap.Core.Interfaces
{
    /// <summary>
    /// 数据库初始化契约
    /// </summary>
    public interface IDatabaseInitService
    {
        /// <summary>
        /// 初始化数据库数据
        /// </summary>
        void Init();

        /// <summary>
        /// 初始化路径码
        /// </summary>
        bool InitPathCode();
    }
}

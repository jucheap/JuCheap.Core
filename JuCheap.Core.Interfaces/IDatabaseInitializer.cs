using System.Threading.Tasks;

namespace JuCheap.Core.Interfaces
{
    /// <summary>
    /// 数据库初始服务接口
    /// </summary>
    public interface IDatabaseInitService
    {
        /// <summary>
        /// 初始化数据库数据
        /// </summary>
        Task<bool> InitAsync();
    }
}

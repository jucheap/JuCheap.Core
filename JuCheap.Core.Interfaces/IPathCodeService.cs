using System.Collections.Generic;

namespace JuCheap.Core.Interfaces
{
    /// <summary>
    /// 路径码服务接口
    /// </summary>
    public interface IPathCodeService
    {
        /// <summary>
        /// 获取路径码
        /// </summary>
        /// <returns></returns>
        IList<string> GetPathCodes();
    }
}

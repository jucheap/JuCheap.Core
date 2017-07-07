using System.Threading.Tasks;
using JuCheap.Core.Models;
using System;
using System.Collections.Generic;

namespace JuCheap.Core.Interfaces
{
    /// <summary>
    /// 应用服务接口
    /// </summary>
    public interface IAppService
    {

        /// <summary>
        /// 添加编辑
        /// </summary>
        /// <param name="dto">模型</param>
        /// <returns></returns>
        Task<Guid> AddOrUpdateAsync(AppDto dto);

        Task<AppDto> GetAsync(Guid id);

        Task<List<AppDto>> GetByUserId(Guid userId);
    }
}

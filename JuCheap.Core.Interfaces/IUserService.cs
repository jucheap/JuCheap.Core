using System.Collections.Generic;
using System.Threading.Tasks;
using JuCheap.Core.Infrastructure;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Filters;
using System;

namespace JuCheap.Core.Interfaces
{
    /// <summary>
    /// 用户契约
    /// </summary>
    public interface IUserService
    {

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="dto">用户模型</param>
        /// <returns></returns>
        Task<Guid> AddAsync(UserAddDto dto);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="dto">用户模型</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(UserUpdateDto dto);

        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<UserDto> FindAsync(Guid id);

            /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="filters">查询过滤参数</param>
        /// <returns></returns>
        Task<PagedResult<UserDto>> SearchAsync(UserFilters filters);
    }
}

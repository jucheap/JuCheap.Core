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
        Task<string> AddAsync(UserAddDto dto);

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
        Task<UserDto> FindAsync(string id);

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="dto">登录信息</param>
        /// <returns></returns>
        Task<UserLoginDto> LoginAsync(LoginDto dto);

            /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(IEnumerable<string> ids);

        /// <summary>
        /// 用户角色授权
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        Task<bool> GiveAsync(string userId, string roleId);

        /// <summary>
        /// 用户角色取消
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        Task<bool> CancelAsync(string userId, string roleId);

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="filters">查询过滤参数</param>
        /// <returns></returns>
        Task<PagedResult<UserDto>> SearchAsync(UserFilters filters);

        /// <summary>
        /// 是否拥有此权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        Task<bool> HasRightAsync(string userId, string url);

        /// <summary>
        /// 记录访问记录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> VisitAsync(VisitDto dto);

        /// <summary>
        /// 检测是否存在用户名
        /// </summary>
        /// <param name="userId">用户ID，可以为空</param>
        /// <param name="loginName">用户名</param>
        /// <returns></returns>
        Task<bool> ExistsLoginNameAsync(string userId, string loginName);
    }
}

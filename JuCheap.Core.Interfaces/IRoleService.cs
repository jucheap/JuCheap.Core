﻿
using System.Collections.Generic;
using System.Threading.Tasks;
using JuCheap.Core.Infrastructure;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Filters;

namespace JuCheap.Core.Interfaces
{
    /// <summary>
    /// 角色契约
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="dto">角色模型</param>
        /// <returns></returns>
        int Add(RoleDto dto);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="dto">角色模型</param>
        /// <returns></returns>
        bool Update(RoleDto dto);

        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        RoleDto Find(int id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        bool Delete(IEnumerable<int> ids);

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="filters">查询过滤参数</param>
        /// <returns></returns>
        PagedResult<RoleDto> Search(RoleFilters filters);

        /// <summary>
        /// 获取角色树
        /// </summary>
        /// <returns></returns>
        List<TreeDto> GetTrees();

        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <returns></returns>
        bool SetRoleMenus(List<RoleMenuDto> datas);

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="dto">角色模型</param>
        /// <returns></returns>
        Task<int> AddAsync(RoleDto dto);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="dto">角色模型</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(RoleDto dto);

        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<RoleDto> FindAsync(int id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(IEnumerable<int> ids);

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="filters">查询过滤参数</param>
        /// <returns></returns>
        Task<PagedResult<RoleDto>> SearchAsync(RoleFilters filters);

        /// <summary>
        /// 获取角色树
        /// </summary>
        /// <returns></returns>
        Task<List<TreeDto>> GetTreesAsync();

        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <returns></returns>
        Task<bool> SetRoleMenusAsync(List<RoleMenuDto> datas);
    }
}

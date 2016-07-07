
using System.Collections.Generic;
using System.Threading.Tasks;
using JuCheap.Core.Infrastructure;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Filters;

namespace JuCheap.Core.Interfaces
{
    /// <summary>
    /// 菜单契约
    /// </summary>
    public interface IMenuService
    {
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        int Add(MenuDto dto);

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        bool Update(MenuDto dto);

        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        MenuDto Find(int id);

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
        PagedResult<MenuDto> Search(MenuFilters filters);

        /// <summary>
        /// 获取用户拥有的权限菜单（不包含按钮）
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        List<MenuDto> GetMyMenus(int userId);

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        List<TreeDto> GetTrees();

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        Task<int> AddAsync(MenuDto dto);

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(MenuDto dto);

        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<MenuDto> FindAsync(int id);

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
        Task<PagedResult<MenuDto>> SearchAsync(MenuFilters filters);

        /// <summary>
        /// 获取用户拥有的权限菜单（不包含按钮）
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        Task<List<MenuDto>> GetMyMenusAsync(int userId);

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        Task<List<TreeDto>> GetTreesAsync();
    }
}

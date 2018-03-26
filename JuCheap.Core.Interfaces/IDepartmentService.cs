using System.Collections.Generic;
using System.Threading.Tasks;
using JuCheap.Core.Infrastructure;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Filters;

namespace JuCheap.Core.Interfaces
{
    /// <summary>
    /// 部门契约
    /// </summary>
    public interface IDepartmentService
    {
        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="dto">部门模型</param>
        /// <returns></returns>
        Task<string> Add(DepartmentDto dto);

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="dto">部门模型</param>
        /// <returns></returns>
        Task<bool> Update(DepartmentDto dto);

        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<DepartmentDto> Find(string id);

        /// <summary>
        /// 根据父部门ID查询
        /// </summary>
        /// <param name="parentId">父部门ID</param>
        /// <returns></returns>
        Task<IList<TreeDto>> FindByParentId(string parentId);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        Task<bool> Delete(IList<string> ids);

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="filters">查询过滤参数</param>
        /// <returns></returns>
        Task<PagedResult<DepartmentDto>> Search(BaseFilter filters);
    }
}

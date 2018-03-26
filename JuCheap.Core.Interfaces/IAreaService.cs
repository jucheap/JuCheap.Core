using System.Collections.Generic;
using System.Threading.Tasks;
using JuCheap.Core.Infrastructure;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Filters;

namespace JuCheap.Core.Interfaces
{
    /// <summary>
    /// 省市区契约
    /// </summary>
    public interface IAreaService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dto">模型</param>
        /// <returns></returns>
        Task<string> Add(AreaDto dto);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dto">模型</param>
        /// <returns></returns>
        Task<bool> Update(AreaDto dto);

        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<AreaDto> Find(string id);

        /// <summary>
        /// 根据父ID查询
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <returns></returns>
        Task<IList<TreeDto>> FindByParentId(string parentId);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        Task<bool> Delete(IList<string> ids);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">区域id，可以为空</param>
        /// <param name="name">区域名称</param>
        /// <returns></returns>
        Task<bool> IsExists(string id, string name);

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="filters">查询过滤参数</param>
        /// <returns></returns>
        Task<PagedResult<AreaDto>> Search(BaseFilter filters);

        /// <summary>
        /// 更新路劲码
        /// </summary>
        Task<bool> UpdatePathCodes();
    }
}

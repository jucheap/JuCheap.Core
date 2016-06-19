using System.Collections.Generic;

namespace JuCheap.Core.Models.Filters
{
    /// <summary>
    /// 角色搜索过滤器
    /// </summary>
    public class RoleFilters : BaseFilter
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// 是否排除当前UserId拥有的角色
        /// </summary>
        public bool ExcludeMyRoles { get; set; }
    }
}

using JuCheap.Core.Infrastructure;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JuCheap.Core.Interfaces
{
    /// <summary>
    /// 站内信服务接口
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// 发送站内信
        /// </summary>
        /// <param name="message"></param>
        Task SendAsync(MessageDto message);

        /// <summary>
        /// 获取站内信
        /// </summary>
        /// <param name="id">站内信Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        Task<MessageDto> GetMessageAsync(string id, string userId);

        /// <summary>
        /// 查看站内信
        /// </summary>
        /// <param name="id">站内信Id</param>
        /// <param name="userId">用户Id</param>
        Task ReadAsync(string id, string userId);

        /// <summary>
        /// 获取站内信列表
        /// </summary>
        /// <param name="filters">过滤器</param>
        /// <returns></returns>
        Task<PagedResult<MessageQueryDto>> SearchAsync(BaseFilter filters);

        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(IList<string> ids);

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        Task<MessageQueryDto> GetDetailsAsync(string messageId);
    }
}

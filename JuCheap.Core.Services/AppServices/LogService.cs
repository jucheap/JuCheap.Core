using System.Linq;
using System.Threading.Tasks;
using JuCheap.Core.Data;
using JuCheap.Core.Infrastructure;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Filters;
using JuCheap.Core.Infrastructure.Extentions;

namespace JuCheap.Core.Services.AppServices
{
    /// <summary>
    /// 日志契约实现
    /// </summary>
    public class LogService : ILogService
    {
        private readonly JuCheapContext _context;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        public LogService(JuCheapContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 获取登录日志
        /// </summary>
        /// <param name="filters">过滤器</param>
        /// <returns></returns>
        public PagedResult<LoginLogDto> SearchLoginLogs(LogFilters filters)
        {
            var dbSet = _context.LoginLogs;
            var query = dbSet.AsQueryable();
            if (filters.keywords.IsNotBlank())
                query = query.Where(item => item.LoginName.Contains(filters.keywords));

            return query.OrderBy(item => item.CreateDateTime)
                .Select(item => new LoginLogDto
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    LoginName = item.LoginName,
                    IP = item.IP,
                    Mac = item.Mac,
                    CreateDateTime = item.CreateDateTime
                }).Paging(filters.page, filters.rows);
        }

        /// <summary>
        /// 获取访问日志
        /// </summary>
        /// <param name="filters">过滤器</param>
        /// <returns></returns>
        public PagedResult<VisitDto> SearchVisitLogs(LogFilters filters)
        {
            var dbSet = _context.PageViews;
            var query = dbSet.AsQueryable();
            if (filters.keywords.IsNotBlank())
                query = query.Where(item => item.LoginName.Contains(filters.keywords));

            return query.OrderBy(item => item.CreateDateTime)
                .Select(item => new VisitDto
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    LoginName = item.LoginName,
                    Ip = item.IP,
                    Url = item.Url,
                    VisitDate = item.CreateDateTime
                }).Paging(filters.page, filters.rows);
        }

        /// <summary>
        /// 获取登录日志
        /// </summary>
        /// <param name="filters">过滤器</param>
        /// <returns></returns>
        public async Task<PagedResult<LoginLogDto>> SearchLoginLogsAsync(LogFilters filters)
        {
            var dbSet = _context.LoginLogs;
            var query = dbSet.AsQueryable();
            if (filters.keywords.IsNotBlank())
                query = query.Where(item => item.LoginName.Contains(filters.keywords));

            return await query.OrderBy(item => item.CreateDateTime)
                .Select(item => new LoginLogDto
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    LoginName = item.LoginName,
                    IP = item.IP,
                    Mac = item.Mac,
                    CreateDateTime = item.CreateDateTime
                }).PagingAsync(filters.page, filters.rows);
        }

        /// <summary>
        /// 获取访问日志
        /// </summary>
        /// <param name="filters">过滤器</param>
        /// <returns></returns>
        public async Task<PagedResult<VisitDto>> SearchVisitLogsAsync(LogFilters filters)
        {
            var dbSet = _context.PageViews;
            var query = dbSet.AsQueryable();
            if (filters.keywords.IsNotBlank())
                query = query.Where(item => item.LoginName.Contains(filters.keywords));

            return await query.OrderBy(item => item.CreateDateTime)
                .Select(item => new VisitDto
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    LoginName = item.LoginName,
                    Ip = item.IP,
                    Url = item.Url,
                    VisitDate = item.CreateDateTime
                }).PagingAsync(filters.page, filters.rows);
        }
    }
}

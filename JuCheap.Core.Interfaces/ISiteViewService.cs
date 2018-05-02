using JuCheap.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JuCheap.Core.Interfaces
{
    /// <summary>
    /// 网站访问量统计接口
    /// </summary>
    public interface ISiteViewService
    {
        /// <summary>
        /// 添加或更新网站访问量
        /// </summary>
        /// <param name="day">日期</param>
        Task<bool> AddOrUpdate();

        /// <summary>
        /// 获取指定时间段内的访问量统计数据
        /// </summary>
        /// <param name="from">开始时间</param>
        /// <param name="to">结束时间</param>
        /// <returns></returns>
        Task<IList<SiteViewDto>> GetSiteViews(DateTime from, DateTime to);
    }
}

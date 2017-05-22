using System;

namespace JuCheap.Core.Models
{
    /// <summary>
    /// 访问记录
    /// </summary>
    public class VisitDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// UserName
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 访问者IP
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 访问时间
        /// </summary>
        public DateTime VisitDate { get; set; }
    }
}

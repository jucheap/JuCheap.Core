using System;
using System.Collections.Generic;

namespace JuCheap.Core.Web.Models
{
    /// <summary>
    /// 静态资源Model
    /// </summary>
    //[Serializable]
    public class StaticFileModel
    {
        /// <summary>
        /// VirtualUrl
        /// </summary>
        public string VirtualUrl { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 资源URLS
        /// </summary>
        public List<string> Srcs { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Desc { get; set; }
    }
}

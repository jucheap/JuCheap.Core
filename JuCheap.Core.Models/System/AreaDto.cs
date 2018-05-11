using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace JuCheap.Core.Models
{
    /// <summary>
    /// 省市区模型
    /// </summary>
    public class AreaDto
    {/// <summary>
     /// Id
     /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        [Display(Name = "区域名称")]
        [Required(ErrorMessage = Message.Required)]
        [MaxLength(50, ErrorMessage = Message.MaxLength)]
        [Remote("Exists", "Area", AdditionalFields = "Id", ErrorMessage = Message.Exists)]
        public string Name { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        [Display(Name = "父级区域")]
        public string ParentId { get; set; }

        /// <summary>
        /// 上级名称
        /// </summary>
        [Display(Name = "父级区域")]
        public string ParentName { get; set; }

        /// <summary>
        /// 全拼
        /// </summary>
        [Display(Name = "全拼")]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string FullSpelling { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>
        [Display(Name = "简拼")]
        [MaxLength(20, ErrorMessage = Message.MaxLength)]
        public string SimpleSpelling { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool Enabled { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        [Display(Name = "层级")]
        public int Level { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Display(Name = "显示顺序")]
        public int DisplaySequence { get; set; }
    }
}

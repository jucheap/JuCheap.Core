using System.ComponentModel.DataAnnotations;
using JuCheap.Core.Models.Enum;
using JuCheap.Core.Infrastructure.Extentions;
using System;

namespace JuCheap.Core.Models
{
    /// <summary>
    /// 菜单模型
    /// </summary>
    public class MenuDto
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        [Display(Name = "上级菜单")]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 上级菜单名称
        /// </summary>
        [Display(Name = "上级菜单")]
        public string ParentName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "菜单名称"), Required, MinLength(2), MaxLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        [Display(Name = "菜单网址"), Required, MaxLength(300)]
        public string Url { get; set; }

        /// <summary>
        /// 排序越大越靠后
        /// </summary>
        [Display(Name = "排序数字")]
        public int Order { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public MenuType Type { get; set; }

        /// <summary>
        /// 菜单类型名称
        /// </summary>
        public string TypeName
        {
            get { return Type.GetDescriptionForEnum(); }
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace JuCheap.Core.Models
{
    /// <summary>
    /// 部门模型
    /// </summary>
    public class DepartmentDto
    {
        public string Id { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Display(Name = "部门名称")]
        [Required(ErrorMessage = Message.Required)]
        [MaxLength(20,ErrorMessage = Message.MaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// 上级部门
        /// </summary>
        [Display(Name = "上级部门")]
        public string ParentId { get; set; }

        /// <summary>
        /// 上级部门名称
        /// </summary>
        [Display(Name = "上级部门")]
        public string ParentName { get; set; }

        /// <summary>
        /// 全称
        /// </summary>
        public string FullName { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace JuCheap.Core.Models
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Display(Name = "角色名称")]
        [Required(ErrorMessage = Message.Required)]
        [MinLength(2, ErrorMessage = Message.MinLength)]
        [MaxLength(20, ErrorMessage = Message.MaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "角色描述")]
        [Required(ErrorMessage = Message.Required)]
        [MinLength(2, ErrorMessage = Message.MinLength)]
        [MaxLength(50, ErrorMessage = Message.MaxLength)]
        public string Description { get; set; }
    }
}

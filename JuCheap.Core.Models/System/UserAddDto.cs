using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JuCheap.Core.Models
{
    /// <summary>
    /// 用户DTO
    /// </summary>
    public class UserAddDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        [Display(Name = "登录账号")]
        [Required(ErrorMessage = Message.Required)]
        [MinLength(4, ErrorMessage = Message.MinLength)]
        [MaxLength(20, ErrorMessage = Message.MaxLength)]
        [RegularExpression("^[^_][a-zA-Z0-9_]*$",ErrorMessage = "登录账号必须是字母、数字或者下划线的组合")]
        [Remote("VerifyLoginName", "User",ErrorMessage = "登录帐号已经存在")]
        public string LoginName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [Display(Name = "真实姓名")]
        [Required(ErrorMessage = Message.Required)]
        [MinLength(2, ErrorMessage = Message.MinLength)]
        [MaxLength(20, ErrorMessage = Message.MaxLength)]
        public string RealName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        [Required(ErrorMessage = Message.Required)]
        [MinLength(5, ErrorMessage = Message.MinLength)]
        [MaxLength(36, ErrorMessage = Message.MaxLength)]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$",ErrorMessage = "请输入正确的邮箱地址")]
        public string Email { get; set; }

        ///<summary>
        /// 密码
        ///</summary>
        [Display(Name = "登录密码")]
        [Required(ErrorMessage = Message.Required)]
        [MinLength(6, ErrorMessage = Message.MinLength)]
        [MaxLength(12, ErrorMessage = Message.MaxLength)]
        public string Password { get; set; }

        ///<summary>
        /// 确认密码
        ///</summary>
        [Display(Name = "确认密码")]
        [Required(ErrorMessage = Message.Required)]
        [MinLength(6, ErrorMessage = Message.MinLength)]
        [MaxLength(12, ErrorMessage = Message.MaxLength)]
        [Compare("Password", ErrorMessage = Message.Compare)]
        public string ConfirmPwd { get; set; }

        [Display(Name = "所属部门")]
        public string DepartmentId { get; set; }
    }
}
